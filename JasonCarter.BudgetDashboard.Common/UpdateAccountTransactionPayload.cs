using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasonCarter.BudgetDashboard.Common
{
    public class UpdateAccountTransactionPayload
    {
        public int AccountTransactionId { get; set; }
        public DateTime Date { get; set; }
        public int TransactionTypeId { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public int TransactionSourceId { get; set; }
        public string TransactionSourceName { get; set; }
    }
}
