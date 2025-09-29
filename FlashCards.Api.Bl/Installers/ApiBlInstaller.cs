using FlashCards.Api.Bl.Facades;
using FlashCards.Api.Bl.Facades.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.Api.Bl.Installers;

public static class ApiBlInstaller
{
    public static void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.Scan(selector =>
            selector.FromAssemblyOf<CardFacade>()
                .AddClasses(classes => classes.AssignableTo(typeof(IFacade<,,>)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());
        
    }
}