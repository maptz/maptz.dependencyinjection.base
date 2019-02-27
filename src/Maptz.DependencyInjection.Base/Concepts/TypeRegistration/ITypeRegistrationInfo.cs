using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Maptz.DependencyInjection.TypeRegistration
{
    public interface ITypeRegistrationInfo
    {
        string Name { get; }
        string Description { get; }
        string Tags { get; }
    }
}