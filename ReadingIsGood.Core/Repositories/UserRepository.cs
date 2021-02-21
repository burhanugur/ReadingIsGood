using ReadingIsGood.Core.Context;
using ReadingIsGood.Core.Repositories.Interface;
using ReadingIsGood.Domain;

namespace ReadingIsGood.Core.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MainDbContext context) : base(context)
        {
        }
    }
}
