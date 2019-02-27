using Microsoft.Extensions.DependencyInjection;
using System;
namespace Maptz.DependencyInjection
{
    /// <summary>
    /// This is not a great idea. When you find a child service, it has to be registered. 
    /// </summary>
    [Obsolete]
    public class ChildServiceProvider : IChildServiceProvider
    {

        internal ChildServiceProvider(IServiceProvider baseService, IServiceProvider thisServiceProvider)
        {
            this.BaseService = baseService;
            this.ThisServiceProvider = thisServiceProvider;
        }

        public object GetService(Type serviceType)
        {
            var fromThis = this.ThisServiceProvider.GetService(serviceType);
            if (fromThis != null)
            {
                return fromThis;
            }
            var fromBase = this.BaseService.GetService(serviceType);
            return fromBase;
        }
        public IServiceProvider BaseService { get; }
        public IServiceProvider ThisServiceProvider { get; }
    }
}