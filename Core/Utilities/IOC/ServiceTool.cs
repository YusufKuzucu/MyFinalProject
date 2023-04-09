using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.IOC
{
    //IServiceCollection=.netin servislerini al ve onları -
    //services.BuildServiceProvider() build et
    //kısaca bu kod bizim web apıde ve ya autofacda oluşturduğumuz ınjecktionları oluştura bilmemize yarıyor
    //bundan böyle biz istediğimiz herhangi bir ınterfacenin servisteki karşılığını bu tool sayesinde alabiliriz  
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
