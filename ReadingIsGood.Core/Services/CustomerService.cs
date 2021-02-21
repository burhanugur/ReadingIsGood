using ReadingIsGood.Core.Model;
using ReadingIsGood.Core.Model.Customer;
using ReadingIsGood.Core.Repositories.Interface;
using ReadingIsGood.Core.Services.Interfaces;
using ReadingIsGood.Domain;
using System;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Services
{
    public class CustomerService : ICustomerService
    {
        #region Member(s)
        private readonly ICustomerRepository customerRepository;
        private readonly IUserService userService;
        #endregion

        #region Constructor(s)
        public CustomerService(
           ICustomerRepository customerRepository,
           IUserService userService)
        {
            this.customerRepository = customerRepository;
            this.userService = userService;
        }
        #endregion

        public async Task<ServiceResponse<bool>> CreateCustomer(CustomerCreateRequest request)
        {
            var userCreate = await this.userService.CreateUser(new Model.User.UserCreateRequest
            {
                Password = request.Password,
                Username = request.Username
            });

            if (!userCreate.IsSuccess)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = userCreate.Message
                };
            }

            var customerCreate = await this.customerRepository.CreateAsync(new Customer
            {
                Name = request.Name,
                Surname = request.Surname,
                UserId = userCreate.Result.Id
            });

            if (customerCreate == null)
            {
                var userRollback = await this.userService.DeleteUser(userCreate.Result.Id);

                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = "The customer could not be created."
                };
            }

            return new ServiceResponse<bool>
            {
                Result = true
            };
        }

        public async Task<ServiceResponse<CustomerDto>> GetCustomerById(Guid id)
        {
            var customer = await this.customerRepository.GetAsync(id).ConfigureAwait(false);

            if (customer == null)
            {
                return new ServiceResponse<CustomerDto>
                {
                    IsSuccess = false,
                    Message = "Customer not found",
                    Result = null
                };
            }

            var result = new CustomerDto
            {
                CreateDate = customer.CreateDate,
                Id = customer.Id,
                ModifyDate = customer.ModifyDate,
                Name = customer.Name,
                Surname = customer.Surname
            };

            return new ServiceResponse<CustomerDto>
            {
                Result = result
            };
        }
    }
}
