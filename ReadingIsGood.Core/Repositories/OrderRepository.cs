using ReadingIsGood.Core.Context;
using ReadingIsGood.Core.Repositories.Interface;
using ReadingIsGood.Domain;

namespace ReadingIsGood.Core.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(MainDbContext context) : base(context)
        {
        }
    }
}
