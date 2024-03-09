using Microsoft.AspNetCore.Identity;

namespace NZWalks.Repositories;

public interface ITokenReposity
{
    string CreateJWTtoken(IdentityUser user, List<string> roles);
}
