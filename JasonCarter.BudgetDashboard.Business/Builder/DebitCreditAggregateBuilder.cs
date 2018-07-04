using JasonCarter.BudgetDashboard.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Builder
{
    public class DebitCreditAggregateBuilder : Builder, IDisposable
    {
        private IDebitCreditAggregate debitCreditAggregate = null;
        public override void Build<T>(T resultClass)
        {
            debitCreditAggregate = (DebitCreditAggregate)resultClass.GetType().GetMethod("Convert").Invoke(resultClass, null);
        }

        public void Dispose()
        {
            debitCreditAggregate=null;
        }

        public override object GetResult()
        {
            return debitCreditAggregate;
        }
    }
}
