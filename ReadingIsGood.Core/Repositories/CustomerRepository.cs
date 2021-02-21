using ReadingIsGood.Core.Context;
using ReadingIsGood.Core.Repositories.Interface;
using ReadingIsGood.Domain;

namespace ReadingIsGood.Core.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(MainDbContext context) : base(context)
        {
        }
    }
}
