using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JasonCarter.BudgetDashboard.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            dynamic data = null;
            using (HttpClient client = new HttpClient())
            {
                var url = "http://localhost:26087/api/Dashboard/GetUtilitiesOverview";

                using (var response = client.GetAsync(url))
                {
                    response.Wait();

                    var result = response.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var responseContent = result.Content;
                        string json = responseContent.ReadAsStringAsync().Result;
                        data = JsonConvert.DeserializeObject(json);
                    }
                }
            }

            var returnData = new
            {
                UtililitesOverview = data
            };

            return View(returnData);
        }
    }
}