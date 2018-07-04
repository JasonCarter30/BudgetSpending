using System.Collections.Generic;
using JasonCarter.BudgetDashboard.Business.Entities;
using JasonCarter.BudgetDashboard.Common;
using Microsoft.AspNetCore.Http;

namespace JasonCarter.BudgetDashboard.Web.Models
{
    public class CurrentMonthTransactionsViewModel : BaseViewModel
    {
        public CurrentMonthTransactionsViewModel(AppConfiguration applicationConfiguration, HttpRequest request) : base(applicationConfiguration, request)
        {
            
        }


        public dynamic Data { get; set; }

        public IEnumerable<ITransactionType> TransactionTypes { get; set; }

        public IEnumerable<ITransactionSource> TransactionSources { get; set; }
    }
}
