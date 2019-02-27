using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace Maptz.DependencyInjection.TypeRegistration
{

    /// <summary>
    /// A lookup repository for types that can be registered by id. 
    /// </summary>
    /// <typeparam name="TInfoType"></typeparam>
    /// <typeparam name="TBaseType"></typeparam>
    public interface ITypeRegistrationRepository<TInfoType, TBaseType> where TInfoType : ITypeRegistrationInfo
    {
        Task RegisterTypeActivatorAsync(string id, TInfoType typeInfo, Func<IServiceProvider, TBaseType> activator);
        Task RegisterTypeAsync<U>(string id, TInfoType typeInfo) where U : TBaseType;
        Task<U> CreateInstanceAsync<U>(string id) where U : TBaseType;
        Task<IDictionary<string, TInfoType>> GetTypeRegistrationDictionary();
    }
}