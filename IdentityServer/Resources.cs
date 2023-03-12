using IdentityModel;
using IdentityServer4.Models;

namespace IdentityServer
{
    internal class Resources
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("DevApi", "DEV Api", new List<string> {JwtClaimTypes.Role}),
                new ApiResource("UatApi", "UAT Api", new List<string> {JwtClaimTypes.Role})
            };
        }
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("DevApi", "DEV Api"),
                new ApiScope("UatApi", "UAT Api")
            };
        }
    }
}
