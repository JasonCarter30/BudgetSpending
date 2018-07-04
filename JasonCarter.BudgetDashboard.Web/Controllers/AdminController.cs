using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using JasonCarter.BudgetDashboard.Business.Admin.Facades;
using JasonCarter.BudgetDashboard.Common;
using JasonCarter.BudgetDashboard.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace JasonCarter.BudgetDashboard.Web.Controllers
{
    //[Produces("application/json")]
    //[Route("api/Admin")]
    public class AdminController : Controller
    {
        private readonly AppConfiguration _appConfiguration;
        private IMemoryCache _memoryCache;

        public AdminController(AppConfiguration appConfiguration, IMemoryCache memoryCache)
        {
            _appConfiguration = appConfiguration;
            _memoryCache = memoryCache;
        }


        public IActionResult CreateDashboardWidget()
        {
            CreateDashboardWidgetModel createDashboardWidgetModel = new CreateDashboardWidgetModel(_appConfiguration, HttpContext.Request);

            return View(createDashboardWidgetModel);
        }


        public IActionResult DataSources()
        {
            AdminViewModel adminViewModel = new AdminViewModel(_appConfiguration, HttpContext.Request);
            return View(adminViewModel);
        }

        public IActionResult Dashboards()
        {
            AdminViewModel adminViewModel = new AdminViewModel(_appConfiguration, HttpContext.Request);
            return View(adminViewModel);
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DataSource(int dataSourceId)
        {
            DataSourceViewModel dataSourceViewModel = new DataSourceViewModel(_appConfiguration, HttpContext.Request);

            dataSourceViewModel.DataSource = new AdminFacade(_appConfiguration, _memoryCache).GetDataSource(dataSourceId);

            return View(dataSourceViewModel);
        }

        public JsonResult GetDataSourceData(int dataSourceId)
        {

            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };

            var result = new AdminFacade(_appConfiguration, _memoryCache).GetDataSourceData(dataSourceId);
            return Json(result, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver(), DateFormatHandling = DateFormatHandling.IsoDateFormat });
        }

        [HttpPost]
        public JsonResult GetDataSourceDataByDataSourceIdAndDataFields(int dataSourceId, List<DataField> dataFields)
        {
         

            

            var result = new AdminFacade(_appConfiguration, _memoryCache).GetDataSourceDataByDataSourceIdAndDataFields(dataSourceId, dataFields);
            return Json(result, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver(), DateFormatHandling = DateFormatHandling.IsoDateFormat });
        }



        public JsonResult GetDataSources()
        {
            var result = new AdminFacade(_appConfiguration, _memoryCache).GetDataSources();
            return Json(result, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
        }

        public JsonResult GetDataSourceColumns(int dataSourceId)
        {
            var result = new AdminFacade(_appConfiguration, _memoryCache).GetDataSourceFields(dataSourceId);
            return Json(result, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
        }

        public JsonResult GetWidgetTypes()
        {
            var result = new AdminFacade(_appConfiguration, _memoryCache).GetWidgetTypes();
            return Json(result, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
        }


        public JsonResult GetDashboards()
        {
            var result = new AdminFacade(_appConfiguration, _memoryCache).GetDashboards();
            return Json(result, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
        }

        public JsonResult GetWidgetConfigurationByWidgetType(int widgetTypeId, int dataSourceId)
        {
            dynamic configuration = new AdminFacade(_appConfiguration, _memoryCache).GetWidgetConfigurationByWidgetType(widgetTypeId, dataSourceId);
            return Json(configuration, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
        }

        public JsonResult GetEditableWidgetConfigurationsByWidgetType(int widgetTypeId)
        {
            dynamic configuration = new AdminFacade(_appConfiguration, _memoryCache).GetEditableWidgetConfigurationsByWidgetType(widgetTypeId);
            return Json(configuration, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
        }
    }
}