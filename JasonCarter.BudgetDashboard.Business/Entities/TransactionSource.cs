using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Entities
{
    public class TransactionSource : ITransactionSource
    {
        public int TransactionSourceId { get; set; }
        public string Name { get; set; }
    }
}
