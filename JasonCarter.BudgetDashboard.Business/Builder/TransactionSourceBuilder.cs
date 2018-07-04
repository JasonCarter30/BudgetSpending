using JasonCarter.BudgetDashboard.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Builder
{
    public class TransactionSourceBuilder : Builder, IDisposable
    {
        private ITransactionSource transactionSource;
        public override void Build<T>(T resultClass)
        {
            transactionSource = (TransactionSource)resultClass.GetType().GetMethod("Convert").Invoke(resultClass, null);
        }

        public void Dispose()
        {
            transactionSource = null;
        }

        public override object GetResult()
        {
            return transactionSource;
        }
    }
}
