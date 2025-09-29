using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.Api.Dal.Installers;

public static class ApiDalInstaller
{
    public static void Install(IServiceCollection services, string connectionString)
    {
        services.AddDbContext<FlashCardsDbContext>(opt
            => opt.UseSqlServer(connectionString));
    }
}