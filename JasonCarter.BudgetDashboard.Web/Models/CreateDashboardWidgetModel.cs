using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JasonCarter.BudgetDashboard.Common;
using Microsoft.AspNetCore.Http;

namespace JasonCarter.BudgetDashboard.Web.Models
{
    public class CreateDashboardWidgetModel : BaseViewModel
    {
        public CreateDashboardWidgetModel(AppConfiguration applicationConfiguration, HttpRequest request) : base(applicationConfiguration, request)
        {
        }
    }
}
