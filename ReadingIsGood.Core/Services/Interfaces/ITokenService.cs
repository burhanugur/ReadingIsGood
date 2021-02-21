using ReadingIsGood.Domain;

namespace ReadingIsGood.Core.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
