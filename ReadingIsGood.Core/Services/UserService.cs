using Microsoft.EntityFrameworkCore;
using ReadingIsGood.Core.Model;
using ReadingIsGood.Core.Model.User;
using ReadingIsGood.Core.Repositories.Interface;
using ReadingIsGood.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Services
{
    public class UserService : IUserService
    {
        #region Member(s)
        private readonly IUserRepository userRepository;
        private readonly ITokenService tokenService;
        private readonly string key = "C84B2811ED58AEA3E898376828CE6";
        #endregion

        #region Constructor(s)
        public UserService(
            IUserRepository userRepository,
            ITokenService tokenService)
        {
            this.userRepository = userRepository;
            this.tokenService = tokenService;
        }
        #endregion

        public async Task<ServiceResponse<TokenDto>> Login(UserLoginRequest request)
        {
            var user = await this.userRepository.GetFirstOrDefaultAsync(
                predicate: x => x.Username == request.Username && x.Password == this.Encrypt(request.Password),
                include: x => x.Include(x => x.Customer)).ConfigureAwait(false);

            if (user == null)
            {
                return new ServiceResponse<TokenDto>
                {
                    IsSuccess = false,
                    Message = "User not found",
                    Result = null
                };
            }

            var token = this.tokenService.CreateToken(user);

            return new ServiceResponse<TokenDto>
            {
                Result = new TokenDto
                {
                    Token = token
                }
            };
        }

        public async Task<ServiceResponse<Domain.User>> CreateUser(UserCreateRequest request)
        {
            var user = await this.userRepository.CreateAsync(new Domain.User
            {
                Username = request.Username,
                Password = this.Encrypt(request.Password)
            });

            if (user == null)
            {
                return new ServiceResponse<Domain.User>
                {
                    IsSuccess = false,
                    Message = "User could not be created."
                };
            }

            return new ServiceResponse<Domain.User>
            {
                Result = user
            };
        }

        public async Task<ServiceResponse<bool>> DeleteUser(Guid id)
        {
            var user = await this.userRepository.GetAsync(id);

            if (user == null)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = "User not found"
                };
            }
            var result = await this.userRepository.DeleteAsync(user);

            if (!result)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = result,
                    Message = "The user could not be deleted."
                };
            }

            return new ServiceResponse<bool>
            {
                Result = result
            };
        }

        #region Private Method(s)
        private string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        private string Decrypt(string cipher)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }
        #endregion
    }
}
