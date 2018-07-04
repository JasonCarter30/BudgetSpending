using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JasonCarter.BudgetDashboard.Web.Models;

using JasonCarter.BudgetDashboard.Common;
using Microsoft.Extensions.Caching.Memory;
using JasonCarter.BudgetDashboard.Business.Facades;

namespace JasonCarter.BudgetDashboard.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppConfiguration _appConfiguration;
        private IMemoryCache _memoryCache;

        public HomeController(AppConfiguration appConfiguration, IMemoryCache memoryCache)
        {
            _appConfiguration = appConfiguration;
            _memoryCache = memoryCache;
        }


        public IActionResult Index()
        {
            BudgetDashboardViewModel budgetDashboardViewModel = new BudgetDashboardViewModel();

            //budgetDashboardViewModel.AccountTransactions = new AccountTransactionFacade(_appConfiguration, _memoryCache).GetAccountTransactionsOrderByDateDescending();
            return View(budgetDashboardViewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
