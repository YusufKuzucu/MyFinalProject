using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IOC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        //veriyi manupule eden methodlarına cacheremoveaspcet uygularsın
        private string _pattern;
        private ICacheManager _cacheManager;
        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }
        //method başarılı olursa git ekle
        protected override void OnSuccess(IInvocation invocation)
        {
            //patterna göre silme işlemi yapıyorlar
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}

