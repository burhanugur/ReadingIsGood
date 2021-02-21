using ReadingIsGood.Core.Model;
using ReadingIsGood.Core.Model.Customer;
using System;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<ServiceResponse<CustomerDto>> GetCustomerById(Guid id);

        Task<ServiceResponse<bool>> CreateCustomer(CustomerCreateRequest request);
    }
}
