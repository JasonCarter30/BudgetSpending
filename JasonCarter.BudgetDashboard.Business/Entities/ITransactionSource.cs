using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Entities
{
    public interface ITransactionSource
    {
        int TransactionSourceId { get; set; }
        string Name { get; set; }
    }
}
