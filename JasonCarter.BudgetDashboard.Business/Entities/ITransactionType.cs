using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Entities
{
    public interface ITransactionType
    {
        int TransactionTypeId { get; set; }
        string Name { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        bool IsDefault { get; set; }
    }
}
