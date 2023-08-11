using webapi.Entities;

namespace webapi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
