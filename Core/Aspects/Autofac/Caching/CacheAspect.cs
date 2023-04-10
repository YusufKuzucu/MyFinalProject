using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IOC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect: MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            //invocation.Method.ReflectedType.FullName} namespace ve classs ismi verir
            //invocation.Method.Name method ismi
            //Notrhwind.Business.IProductService.GetAll
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            //paramterleri listeye çevir tabiki parametresi varsa
            var arguments = invocation.Arguments.ToList();
            //parametre varsa bu şelikde ekliyor parametre yoksa null
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            //daha önce böyle key varmı
            if (_cacheManager.IsAdd(key))
            {
                //varsa methodu çalıştırmadan geri döm
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            //invocationu çalıştır methodu devam ettir
            invocation.Proceed();
            //veritabanından veri getirdi
            //bu özellikleride eklemiş olcak
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
