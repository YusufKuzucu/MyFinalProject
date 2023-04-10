using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IOC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.Aspects.Autofac.Performance
{

    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;//timer

        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();//ınstance ekliyoruz
        }


        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();//methodun önündebaşlattık
        }

        protected override void OnAfter(IInvocation invocation)
        {
            //method bittiğinde geçene süre hesaplanıyor
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");
            }
            _stopwatch.Reset();
        }
    }
}
