using JasonCarter.BudgetDashboard.Business.Entities;
using JasonCarter.BudgetDashboard.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.DataEntities
{
    public class TransactionSourceResult : IDapperResult<ITransactionSource>
    {
        public int TransactionSourceId { get; set; }
        public string Name { get; set; }

        public ITransactionSource Convert()
        {
            ITransactionSource transactionSource = new TransactionSource()
            {
                TransactionSourceId = TransactionSourceId,
                Name = Name
            };

            return transactionSource;
        }
    }
}
