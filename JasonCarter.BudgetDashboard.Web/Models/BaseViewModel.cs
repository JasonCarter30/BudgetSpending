using JasonCarter.BudgetDashboard.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JasonCarter.BudgetDashboard.Web.Models
{
    public class BaseViewModel
    {
        public string BaseURL { get; set; }

        public AppConfiguration AppConfiguration { get; set; }

        public BaseViewModel(AppConfiguration applicationConfiguration, HttpRequest request)
        {
            AppConfiguration = applicationConfiguration;

            BaseURL = $"{request.Scheme}://{request.Host}{request.PathBase}";
        }
    }
}
