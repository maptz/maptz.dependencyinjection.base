using Microsoft.Extensions.DependencyInjection;
using System;
namespace Maptz.DependencyInjection
{

    public static class IServiceCollectionExtensions
    {

        public static IServiceProvider BuildChildServiceProvider(this IServiceCollection serviceCollection, IServiceProvider baseServiceProvider)
        {
            var thisServiceProvider = serviceCollection.BuildServiceProvider();
            return new ChildServiceProvider(baseServiceProvider, thisServiceProvider);
        }
    }
}