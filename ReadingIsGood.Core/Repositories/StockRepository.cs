using ReadingIsGood.Core.Context;
using ReadingIsGood.Core.Repositories.Interface;
using ReadingIsGood.Domain;

namespace ReadingIsGood.Core.Repositories
{
    public class StockRepository : RepositoryBase<Stock>, IStockRepository
    {
        public StockRepository(MainDbContext context) : base(context)
        {
        }
    }
}
