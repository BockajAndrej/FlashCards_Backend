using System.Security.Claims;
using IdentityModel;
using FlashCards.Identity.App.Data;
using FlashCards.Identity.App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FlashCards.Identity.App;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var jakub = userMgr.FindByNameAsync("jakub").Result;
            if (jakub == null)
            {
                jakub = new ApplicationUser
                {
                    UserName = "jakub",
                    Email = "jakubDaky@email.com",
                    EmailConfirmed = true,
                };
                var result = userMgr.CreateAsync(jakub, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(jakub, new Claim[]
                {
                    new (JwtClaimTypes.Name, "Jakub Daky"),
                    new (JwtClaimTypes.GivenName, "Jakub"),
                    new (JwtClaimTypes.FamilyName, "Daky"),
                    new (JwtClaimTypes.Picture, "https://encrypted-tbn3.gstatic.com/licensed-image?q=tbn:ANd9GcRdnKLy23zqYgErMEuIhYKTdQEkZBw_qELGZTPfUmAx3B1NnG2ze3bMmQBzYJYl0onCuKGYKeT6V2_4PQwAvHNKdMX43YoeEI0U_LjbQLx0iYaSzgU"),
                    new (JwtClaimTypes.Role, "user"),
                    new (JwtClaimTypes.WebSite, "http://jakub.com"),
                }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                Log.Debug("jakub created");
            }
            else
            {
                Log.Debug("jakub already exists");
            }

            var andrej = userMgr.FindByNameAsync("andrej").Result;
            if (andrej == null)
            {
                andrej = new ApplicationUser
                {
                    UserName = "andrej",
                    Email = "andrejOny@email.com",
                    EmailConfirmed = true
                };
                var result = userMgr.CreateAsync(andrej, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(andrej, new Claim[]
                {
                    new (JwtClaimTypes.Name, "andrej Ony"),
                    new (JwtClaimTypes.GivenName, "andrej"),
                    new (JwtClaimTypes.FamilyName, "Ony"),
                    new (JwtClaimTypes.WebSite, "http://andrej.com"),
                    new (JwtClaimTypes.Picture, "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcTw2XDqVGTL5K28a2Lwyi4AJ-JpHpt9994ek4u3cYWnrS9sgR3Yv7XyB5xY_l0Q"),
                    new (JwtClaimTypes.Role, "admin"),
                    new ("location", "somewhere")
                }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                Log.Debug("andrej created");
            }
            else
            {
                Log.Debug("andrej already exists");
            }
        }
    }
}