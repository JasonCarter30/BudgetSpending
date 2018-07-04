using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasonCarter.BudgetDashboard.Business.Entities
{
    public class AccountTransaction : IAccountTransaction
    {
        public int AccountTransactionId { get; set; }
        public ITransactionType TransactionType { get; set; }
        public int TransactionSourceId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public  string Notes { get; set; }
        public ITransactionSource TransactionSource { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
}
