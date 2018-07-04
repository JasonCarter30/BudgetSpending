using JasonCarter.BudgetDashboard.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JasonCarter.BudgetDashboard.Web.Models
{
    public class BudgetDashboardViewModel
    {
        public IEnumerable<IAccountTransaction> AccountTransactions { get; set; }

        public IEnumerable<ITransactionType> TransactionTypes { get; set; }
    }
}
