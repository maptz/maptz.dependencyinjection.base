using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace Maptz.DependencyInjection.TypeRegistration
{


    public class TypeRegistrationRepositorySettings<TInfoType, TBaseType> where TInfoType : ITypeRegistrationInfo
    {
        public Func<TypeRegistrationRepository<TInfoType, TBaseType>, Task> OnInitialized { get; set; }
    }
}