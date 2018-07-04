using JasonCarter.BudgetDashboard.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Builder
{
    public class TransactionTypeBuilder : Builder, IDisposable
    {
        internal ITransactionType transactionType;
        public override void Build<T>(T resultClass)
        {
            transactionType = (TransactionType)resultClass.GetType().GetMethod("Convert").Invoke(resultClass, null);
        }

        public void Dispose()
        {
            transactionType = null;
        }

        public override object GetResult()
        {
            return transactionType;
        }
    }
}
