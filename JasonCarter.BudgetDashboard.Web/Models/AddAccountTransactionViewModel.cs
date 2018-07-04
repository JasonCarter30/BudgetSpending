using JasonCarter.BudgetDashboard.Business.Entities;
using JasonCarter.BudgetDashboard.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JasonCarter.BudgetDashboard.Web.Models
{
    public class AddAccountTransactionViewModel:BaseViewModel
    {
        public AddAccountTransactionViewModel(AppConfiguration applicationConfiguration, HttpRequest request) : base(applicationConfiguration, request)
        {
        }

        public IEnumerable<ITransactionType> TransactionTypes { get; set; }
    }
}
