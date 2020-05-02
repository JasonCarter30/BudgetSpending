using System.Linq;
using JasonCarter.BudgetDashboard.Business.Facades;
using JasonCarter.BudgetDashboard.Common;
using JasonCarter.BudgetDashboard.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Serialization;

namespace JasonCarter.BudgetDashboard.Web.Controllers
{
    public class AccountTransactionsController : Controller
    {

        private readonly AppConfiguration _appConfiguration;
        private IMemoryCache _memoryCache;

        public AccountTransactionsController(AppConfiguration appConfiguration, IMemoryCache memoryCache)
        {
            _appConfiguration = appConfiguration;
            _memoryCache = memoryCache;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CurrentMonthTransactions()
        {
            CurrentMonthTransactionsViewModel currentMonthTransactionsViewModel = new CurrentMonthTransactionsViewModel(_appConfiguration, HttpContext.Request);

            AccountTransactionFacade accountTransactionFacade = new AccountTransactionFacade(_appConfiguration, _memoryCache);

            currentMonthTransactionsViewModel.Data = accountTransactionFacade.GetCurrentMonthAccountTransactions();
            currentMonthTransactionsViewModel.TransactionTypes = accountTransactionFacade.GetTransactionTypes();

            return View(currentMonthTransactionsViewModel);
        }

        public IActionResult AllAccountTransactions()
        {
            BudgetDashboardViewModel budgetDashboardViewModel = new BudgetDashboardViewModel();
            AccountTransactionFacade accountTransactionFacade = new AccountTransactionFacade(_appConfiguration, _memoryCache);

            budgetDashboardViewModel.TransactionTypes = accountTransactionFacade.GetTransactionTypes();

            string transactionType = budgetDashboardViewModel.TransactionTypes.Where(x => x.IsDefault).First().Name;

            if (HttpContext.Request.QueryString.HasValue)
            {
                if (HttpContext.Request.QueryString.Value.Contains("TransactionType"))
                {
                    
                }
            }


            budgetDashboardViewModel.AccountTransactions = new AccountTransactionFacade(_appConfiguration, _memoryCache).GetAccountTransactionsOrderByDateDescending();
            

            return View(budgetDashboardViewModel);
        }

        public IActionResult Overview()
        {
            OverviewViewModel budgetDashboardViewModel = new OverviewViewModel(_appConfiguration, HttpContext.Request);
            


            return View(budgetDashboardViewModel);
        }

        public IActionResult AddAccountTransaction()
        {
            AddAccountTransactionViewModel addAccountTransactionViewModel = new AddAccountTransactionViewModel(_appConfiguration, HttpContext.Request);

            AccountTransactionFacade accountTransactionFacade = new AccountTransactionFacade(_appConfiguration, _memoryCache);

            addAccountTransactionViewModel.TransactionTypes = accountTransactionFacade.GetTransactionTypes();






            return View(addAccountTransactionViewModel);
        }

        public void InsertAccountTransaction(InsertAccountTransactionPayload payloadData)
        {
            AccountTransactionFacade accountTransactionFacade = new AccountTransactionFacade(_appConfiguration, _memoryCache);

            accountTransactionFacade.InsertAccountTransaction(payloadData);
        }


        public void UpdateAccountTransaction(UpdateAccountTransactionPayload payloadData)
        {
            AccountTransactionFacade accountTransactionFacade = new AccountTransactionFacade(_appConfiguration, _memoryCache);

            accountTransactionFacade.UpdateAccountTransaction(payloadData);
        }

        

        public JsonResult GetTransactionSourcesByLookupValue(string lookupValue)
        {
            var result = new AccountTransactionFacade(_appConfiguration, _memoryCache).GetTransactionSources(lookupValue);

            return Json(result);
        }


        public JsonResult GetYearlyTransactionSourceSummary(int year)
        {
            var result = new AccountTransactionFacade(_appConfiguration, _memoryCache).GetYearlyTransactionSourceSummary(year);
            return Json(result);
        }


        public JsonResult GetDebitCreditTotalsGroupByMonthByYear(int year)
        {
            var result = new AccountTransactionFacade(_appConfiguration, _memoryCache).GetDebitCreditTotalsGroupByMonthByYear();
            return Json(result);
        }

        public JsonResult GetTransactionsByTransactionSourceIdTransactionTypeIdMonthYear(int transactionSourceId, int transactionTypeId, int month, int year)
        {
            var result = new AccountTransactionFacade(_appConfiguration, _memoryCache).GetTransactionsByTransactionSourceIdTransactionTypeIdMonthYear(transactionSourceId, transactionTypeId, month, year);
            return Json(result);
        }


        public JsonResult GetGasSpendingDetailsByYearAndMonth(int year, int month)
        {
            var result = new AccountTransactionFacade(_appConfiguration, _memoryCache).GetGasSpendingDetailsByYearAndMonth(year, month);
            return Json(result);
        }

        public JsonResult GetMonthlyGasSummaryByYear(int year)
        {
            var result = new AccountTransactionFacade(_appConfiguration, _memoryCache).GetRollingTwelveMonthGasSummary();
            return Json(result, new Newtonsoft.Json.JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
        }

        [HttpGet]
        public JsonResult GetAccountTransactionByAccountTransactionId(int accountTransactionId)
        {
            var result = new AccountTransactionFacade(_appConfiguration, _memoryCache).GetAccountTransactionByAccountTransactionId(accountTransactionId);
            return Json(result, new Newtonsoft.Json.JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
        }
    }
}