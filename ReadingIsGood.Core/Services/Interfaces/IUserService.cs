using ReadingIsGood.Core.Model;
using ReadingIsGood.Core.Model.User;
using ReadingIsGood.Domain;
using System;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<TokenDto>> Login(UserLoginRequest request);

        Task<ServiceResponse<User>> CreateUser(UserCreateRequest request);

        Task<ServiceResponse<bool>> DeleteUser(Guid id);
    }
}
