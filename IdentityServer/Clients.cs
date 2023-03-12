using IdentityModel;
using IdentityServer4.Models;

namespace IdentityServer
{
    internal class Clients
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "Admin",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"DevApi", "UatApi"},
                    ClientSecrets = { new Secret("adminSecret".Sha256())},
                    Claims = new List<ClientClaim>
                    {
                        new ClientClaim(JwtClaimTypes.Role, "admin"),
                        new ClientClaim(JwtClaimTypes.Role, "manager"),
                        new ClientClaim(JwtClaimTypes.Role, "user")
                    },
                    ClientClaimsPrefix = string.Empty
                },
                new Client
                {
                    ClientId = "Manager",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"UatApi"},
                    ClientSecrets = { new Secret("managerSecret".Sha256())},
                    Claims = new List<ClientClaim>
                    {
                        new ClientClaim(JwtClaimTypes.Role, "manager"),
                        new ClientClaim(JwtClaimTypes.Role, "user")
                    },
                    ClientClaimsPrefix = string.Empty
                },
                new Client
                {
                    ClientId = "User",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"UatApi"},
                    ClientSecrets = { new Secret("userSecret".Sha256())},
                    Claims = new List<ClientClaim>
                    {
                        new ClientClaim(JwtClaimTypes.Role, "user")
                    },
                    ClientClaimsPrefix = string.Empty
                }
            };
        }
    }
}
