using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        //invocation method demek
        //intercept bu şablonu çalıştır
        public override void Intercept(IInvocation invocation)
        {
            //şablon 
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    //methodu çalıştır içini
                    invocation.Proceed();
                    transactionScope.Complete();
                }
                catch (System.Exception )
                {
                    //dipos at 
                    transactionScope.Dispose();
                    //işlem başarısız oldu diye uyar
                    throw;
                }
            }
        }
    }
}
