using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using JasonCarter.BudgetDashboard.Common;
using Microsoft.AspNetCore.Http;

namespace JasonCarter.BudgetDashboard.Web.Models
{
    public class DataSourceViewModel : BaseViewModel
    {
        public DataSourceViewModel(AppConfiguration applicationConfiguration, HttpRequest request) : base(applicationConfiguration, request)
        {
        }

        public DataSource DataSource { get; set; }
    }
}
