using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Reflection;

namespace Core.Utilities.Interceptors
{
    public abstract partial class MethodInterception
    {
        public class AspectInterceptorSelector : IInterceptorSelector
        {
            public Microsoft.EntityFrameworkCore.Diagnostics.IInterceptor[] SelectInterceptors(Type type, MethodInfo method, Microsoft.EntityFrameworkCore.Diagnostics.IInterceptor[] interceptors)
            {
                var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                    (true).ToList();
                var methodAttributes = type.GetMethod(method.Name)
                    .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
                classAttributes.AddRange(methodAttributes);

                return (Microsoft.EntityFrameworkCore.Diagnostics.IInterceptor[])classAttributes.OrderBy(x => x.Priority).ToArray();
            }

            public Castle.DynamicProxy.IInterceptor[] SelectInterceptors(Type type, MethodInfo method, Castle.DynamicProxy.IInterceptor[] interceptors)
            {
                throw new NotImplementedException();
            }
        }
    }

}
