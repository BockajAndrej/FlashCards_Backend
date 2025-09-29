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
                    new (JwtClaimTypes.Picture, "https://www.gravatar.com/avatar/2c7d99fe281ecd3bcd65ab915bac6dd5?s=250"),
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
                    new (JwtClaimTypes.Name, "Andrej Ony"),
                    new (JwtClaimTypes.GivenName, "Andrej"),
                    new (JwtClaimTypes.FamilyName, "Ony"),
                    new (JwtClaimTypes.WebSite, "http://andrej.com"),
                    new (JwtClaimTypes.Picture, "https://robohash.org/mail@ashallendesign.co.uk"),
                    new (JwtClaimTypes.Role, "admin"),
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