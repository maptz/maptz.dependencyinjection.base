using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace Maptz.DependencyInjection.TypeRegistration
{

    public class TypeRegistrationRepository<TInfoType, TBaseType> : ITypeRegistrationRepository<TInfoType, TBaseType> where TInfoType : ITypeRegistrationInfo
    {
        public TypeRegistrationRepository(IOptions<TypeRegistrationRepositorySettings<TInfoType, TBaseType>> settings, IServiceProvider serviceProvider)
        {
            this.Settings = settings.Value;
            this.ConverterDictionary = new Dictionary<string, DictionaryItem>();
            this.ServiceProvider = serviceProvider;

            if (this.Settings.OnInitialized != null)
            {
                this.Settings.OnInitialized(this).Wait();
            }
        }

        public class DictionaryItem
        {
            public TInfoType ConverterInfo { get; set; }
            public Func<IServiceProvider, TBaseType> ConverterActivator { get; set; }
        }

        public TypeRegistrationRepositorySettings<TInfoType, TBaseType> Settings { get; }

        public Dictionary<string, DictionaryItem> ConverterDictionary { get; }
        public IServiceProvider ServiceProvider { get; }

        public Task RegisterTypeActivatorAsync(string id, TInfoType info, Func<IServiceProvider, TBaseType> activator)
        {
            return Task.Run(() =>
            {
                if (info == null) throw new ArgumentNullException(nameof(info));
                if (activator == null) throw new ArgumentNullException(nameof(activator));
                if (this.ConverterDictionary.ContainsKey(id)) throw new InvalidOperationException($"A converter is already registered with id {id}");

                this.ConverterDictionary.Add(id, new DictionaryItem
                {
                    ConverterActivator = activator,
                    ConverterInfo = info
                });
            });
        }


        public Task RegisterTypeAsync<T>(string id, TInfoType info) where T : TBaseType
        {
            Func<IServiceProvider, TBaseType> activator = (sp) =>
            {
                return sp.GetRequiredService<T>();
            };
            return this.RegisterTypeActivatorAsync(id, info, activator);
        }

        public Task<T> CreateInstanceAsync<T>(string id) where T : TBaseType
        {
            return Task.Run(() =>
            {
                if (!this.ConverterDictionary.ContainsKey(id)) throw new KeyNotFoundException(id);
                var dictItem = this.ConverterDictionary[id];
                var retval = (T)dictItem.ConverterActivator(this.ServiceProvider);
                return retval;
            });
        }

        public Task<IDictionary<string, TInfoType>> GetTypeRegistrationDictionary()
        {
            return Task.Run(() =>
            {
                IDictionary<string, TInfoType> retval = new Dictionary<string, TInfoType>();
                foreach (var kvp in this.ConverterDictionary)
                {
                    retval.Add(kvp.Key, kvp.Value.ConverterInfo);
                }
                return retval;
            });
        }
    }
}