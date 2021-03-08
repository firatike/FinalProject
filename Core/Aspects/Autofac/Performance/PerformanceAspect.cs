using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch; 

        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }


        protected override void OnBefore(IInvocation invocation) //metodun önünde kronometreyi başlatıyoruz
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation) //metot bittiğindede
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval) //o ana kadar geçen süre 5 saniyeden büyükse
            {
                Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");
                //Burda tabiki console log olarak yazmış Engin Hoca. Biz burda artık mailmi göndeririz, logmu alırız o bizlerin bileceği iş
            }
            _stopwatch.Reset();
        }
    }
}
