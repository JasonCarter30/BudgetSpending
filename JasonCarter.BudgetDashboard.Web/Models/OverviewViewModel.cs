using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JasonCarter.BudgetDashboard.Common;
using Microsoft.AspNetCore.Http;

namespace JasonCarter.BudgetDashboard.Web.Models
{
    public class OverviewViewModel : BaseViewModel
    {
        public OverviewViewModel(AppConfiguration applicationConfiguration, HttpRequest request) : base(applicationConfiguration, request)
        {
        }
    }
}
