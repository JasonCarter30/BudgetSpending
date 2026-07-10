using System;
using System.Threading.Tasks;
using JasonCarter.BudgetDashboard.Business.Facades;
using JasonCarter.BudgetDashboard.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace JasonCarter.BudgetSpending.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly AppConfiguration _appConfiguration;
        private IMemoryCache _memoryCache;

        public DashboardController(AppConfiguration appConfiguration, IMemoryCache memoryCache)
        {
            _appConfiguration = appConfiguration;
            _memoryCache = memoryCache;
        }


        [Route("GetUtilitiesOverview")]
        [HttpGet]
        public async Task<ActionResult> GetUtilitiesOverview() {
            ActionResult actionResult = null;

            try
            {
                using(var facade = new DashboardFacade(_appConfiguration, _memoryCache)){
                    var data = await facade.GetUtilitiesOverview();
                    actionResult = Ok(data);
                }
            }
            catch (Exception e)
            {
                string message = "There was an error getting Utilities Overview.";
                actionResult = BadRequest(message);
            }

            return actionResult;
        }
    }
}