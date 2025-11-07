using Duende.IdentityServer.Models;
using IdentityModel;

namespace FlashCards.Identity.App;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
            {
                UserClaims =
                {
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.WebSite
                }
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new("FlashCardsApiScope")
            {
                UserClaims = new[] {"role"}
            }
        };

    public static IEnumerable<Client> Clients(WebApplicationBuilder configuration) =>
        new Client[]
        {
            // interactive client using code flow + pkce
            new()
            {
                ClientId = "PostmanClientId",
                ClientName = "Postman Client",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                
                RedirectUris = { "https://oauth.pstmn.io/v1/callback" },
                PostLogoutRedirectUris = { "https://oauth.pstmn.io/v1/callback" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "FlashCardsApiScope" }
            },
            new()
            {
                ClientId = "FlashCardClientId",
                ClientName = "TaskManagerClientName",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                
                RedirectUris = { configuration.Configuration["Clients:RedirectUris"] ?? throw new InvalidOperationException() },
                PostLogoutRedirectUris = { configuration.Configuration["Clients:PostLogoutRedirectUris"] ?? throw new InvalidOperationException() },
                
                AllowedCorsOrigins = { configuration.Configuration["Clients:AllowedCorsOrigins"] ?? throw new InvalidOperationException() },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "FlashCardsApiScope" }
            },
        };
}