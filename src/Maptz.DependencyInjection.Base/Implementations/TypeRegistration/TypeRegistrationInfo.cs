using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace Maptz.DependencyInjection.TypeRegistration
{

    public class TypeRegistrationInfo : ITypeRegistrationInfo
    {
        public TypeRegistrationInfo(string name, string description, string tags)
        {
            this.Name = name;
            this.Description = description;
            this.Tags = tags;
        }
        public string Name { get; }
        public string Description { get; }
        public string Tags { get; }
    }
}