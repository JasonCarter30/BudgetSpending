using JasonCarter.BudgetDashboard.Common;
using JasonCarter.BudgetDashboard.Data.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JasonCarter.BudgetDashboard.Business.Facades
{
    public class DashboardFacade : IDisposable
    {
        private AppConfiguration _appConfiguration;
        private IMemoryCache _memoryCache;
        
        public DashboardFacade(AppConfiguration appConfiguration, IMemoryCache memoryCache)
        {
            _appConfiguration = appConfiguration;
            _memoryCache = memoryCache;
        }

        public void Dispose()
        {
            _appConfiguration = null;
            _memoryCache = null;
        }

        public async Task<dynamic> GetUtilitiesOverview()
        {
            dynamic data = await getUtiltiesOverview();
            return data;
        }

        private async Task<IEnumerable<dynamic>> getUtiltiesOverview()
        {
            var returnValue = await Task.Run(() =>
            {
                IEnumerable<dynamic> items = null;

                using (DataRepository dataRepositiory = new DataRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
                {
                    items = dataRepositiory.ExecuteStoredProcedure("sp_GetUtilitiesOverview");
                }

                return items;
            });

            return returnValue;
        }
    }
}
