using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IOC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection servicesCollection)
        {
            servicesCollection.AddMemoryCache();//IMemoryCache karşılık gelcek
            servicesCollection.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            servicesCollection.AddSingleton<ICacheManager,MemoryCacheManager>();//microsoftun kendi implementasyonu
            servicesCollection.AddSingleton<Stopwatch>();
        }
    }
}
