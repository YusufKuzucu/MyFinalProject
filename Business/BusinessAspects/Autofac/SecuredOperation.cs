using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IOC;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.BusinessAspects.Autofac
{
    //aspecti inject edemiyoruz
    //ServiceTool= .netin kendi servis mimarisini yadık
    //


    //SecuredOperation=JWt için 
    //IHttpContextAccessor=jwt göndererek istek yapıyoruz ya her istek için http context oluşur
    //IHttpContextAccessor=asp.net core içinden gelir
    //herkese bir treat oluşur
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {//claimlerin içinde ilgili rol varsa
                if (roleClaims.Contains(role))
                {
                    return;//methodu çalıştırmaya devam et
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
