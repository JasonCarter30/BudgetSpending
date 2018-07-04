using JasonCarter.BudgetDashboard.Business.Entities;
using JasonCarter.BudgetDashboard.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasonCarter.BudgetDashboard.Business.DataEntities
{
    internal class AccountTransactionResult : IDapperResult<IAccountTransaction>
    {
        public int AccountTransactionId { get; set; }
        public int TransactionTypeId { get; set; }
        public int TransactionSourceId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        public IAccountTransaction Convert()
        {
            IAccountTransaction accountTransaction = new AccountTransaction()
            {
                AccountTransactionId = AccountTransactionId,
                TransactionSourceId = TransactionSourceId,
                Date = Date,
                Amount = Amount,
                Notes = Notes,
            };

            return accountTransaction;
        }
    }
}
