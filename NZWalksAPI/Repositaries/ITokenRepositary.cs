using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositaries
{
    public interface ITokenRepositary
    {
        public string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
