using Core.Utilities.IOC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    //extenseions da classın static olması lazım 
    //genişletiyoruz şuan
    //IServiceCollection=bizim apimizin servis bağımlılıklarını eklediğimiz yada araya girmesini istediğimiz servislerin eklediğimiz koleksiyonun ta kendisi
    //bu hareket bizim core katmanıda dahil bütün injecktionları bir araya topladık
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection servicesCollection, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(servicesCollection);
            }
            return ServiceTool.Create(servicesCollection);
        }
    }
}
