using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using static Core.Utilities.Interceptors.MethodInterception;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModel:Module
    {
        //uygulama ayağı kalktıgından alttakı kodlar  çalışcak
        protected override void Load(ContainerBuilder builder)
        {
            //registerType,=services.AddSingleton<IProductService, ProductManager>(); buna karşılık geliyor
            //birisi senden IProductService isterse EfProductDalı register ET ınstanceyi ver
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();


        }
    

    }
}
