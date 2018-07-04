using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasonCarter.BudgetDashboard.Business.Entities
{
    public interface IAccountTransaction
    {
        int AccountTransactionId { get; set; }
        ITransactionType TransactionType { get; set; }
        ITransactionSource TransactionSource { get; set; }
        int TransactionSourceId { get; set; }
        DateTime Date { get; set; }
        Decimal Amount { get; set; }
        Decimal Debit { get; set; }
        Decimal Credit { get; set; }
        string Notes { get; set; }
    }
}
