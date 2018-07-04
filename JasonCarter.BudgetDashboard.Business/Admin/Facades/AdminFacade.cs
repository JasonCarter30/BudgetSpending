using JasonCarter.BudgetDashboard.Business.Admin.Builder;
using JasonCarter.BudgetDashboard.Business.Admin.DataEntities;
using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using JasonCarter.BudgetDashboard.Business.DataEntities;
using JasonCarter.BudgetDashboard.Business.Helpers;
using JasonCarter.BudgetDashboard.Common;
using JasonCarter.BudgetDashboard.Data.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace JasonCarter.BudgetDashboard.Business.Admin.Facades
{
    public class AdminFacade
    {
        private AppConfiguration _appConfiguration;
        private IMemoryCache _memoryCache;
        private static readonly object CacheLockObject = new object();


        public AdminFacade(AppConfiguration appConfiguration, IMemoryCache memoryCache)
        {
            _appConfiguration = appConfiguration;
            _memoryCache = memoryCache;
        }

        internal IWidgetConfigurationProperty GetWidgetConfigurationPropertyLite(int widgetConfigurationPropertyId)
        {
            var result = getWidgetConfigurationPropertiesLite().Where(x => x.WidgetConfigurationPropertyId == widgetConfigurationPropertyId).FirstOrDefault();
            return result;
        }

      

        internal ISupportedValue GetSupportedValueBySupportedValueId(int supportedValueId)
        {
            var result = getSupportedValues().Where(x => x.SupportedValueId == supportedValueId).FirstOrDefault();
            return result;
        }

        internal IEnumerable<IWidgetConfigurationPropertySupportedValue> GetWidgetConfigurationPropertySupportedValuesByWidgetConfigurationPropertyId(int widgetConfigurationPropertyId)
        {
            var result = getWidgetConfigurationPropertySupportedValues().Where(x => x.WidgetConfigurationProperty.WidgetConfigurationPropertyId == widgetConfigurationPropertyId);
            return result;
        }

        internal IWidgetConfiguration GetWidgetConfigurationByWidgetConfigurationIdLite(int widgetConfigurationId)
        {
            var result = getWidgetConfigurationsLite().Where(x => x.WidgetConfigurationId == widgetConfigurationId).FirstOrDefault();
            return result;
        }

        //public static dynamic GetDataSourceFields(int dataSourceId)
        //{
        //    dynamic items = null;

        //    using (SqlConnection sqlconnection = new SqlConnection(connectionString))
        //    {
        //        sqlconnection.Open();
        //        string sql = string.Format("sp_describe_first_result_set N'{0}'", storedProcedure);
        //        items = sqlconnection.Query(string.Format(sql));
        //    }

        //    return items;
        //}

        //public IEnumerable<IDataSourceField> GetDataSourceFields(int dataSourceId)
        public dynamic GetDataSourceFields(int dataSourceId)
        {
            DataSource dataSource = GetDataSource(dataSourceId);

            dynamic returnValue = null;

            returnValue = BudgetDashboard.Data.DataAccess.Helper.GetStoredProcedureColumns(_appConfiguration["DatabaseConnectionString"].Value.ToString(), dataSource.CommandText);

            IEnumerable<DashboardResult> items = null;
            using (DataRepository dataRepository = new DataRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
            {
                items = dataRepository.GetAll<DashboardResult>(typeof(Dashboard), "Dashboard").ToList();
            }

            returnValue = BudgetDashboard.Data.DataAccess.Helper.GetStoredProcedureColumns(_appConfiguration["DatabaseConnectionString"].Value.ToString(), dataSource.CommandText);

            //dynamic hey = result.Select(x => new
            //{
            //    Name = x.Name
            //});


            return returnValue;
        }



        internal IProperty GetPropertyByPropertyId(int propertyId)
        {
            var result = getProperties().Where(x => x.PropertyId == propertyId).FirstOrDefault();
            return result;
        }

        internal IWidgetType GetWidgetTypeByWidgetTypeId(int widgetTypeId)
        {
            var result = getWidgetTypes().Where(x => x.WidgetTypeId == widgetTypeId).FirstOrDefault();
            return result;
        }

        internal IWidgetConfiguration GetWidgetConfigurationByWidgetConfigurationId(int widgetConfigurationId)
        {
            var result = getWidgetConfigurations().Where(x => x.WidgetConfigurationId == widgetConfigurationId).FirstOrDefault();
            return result;
        }

        internal IObjectType GetObjectTypeIdByObjectTypeId(int objectTypeId)
        {
            var result = getObjectTypes().Where(x => x.ObjectTypeId == objectTypeId).FirstOrDefault();
            return result;
        }

        internal IEnumerable<IWidgetConfigurationProperty> GetWidgetConfigurationPropertiesByWidgetConfigurationId(int widgetConfigurationId)
        {
            var result = getWidgetConfigurationProperties().Where(x => x.WidgetConfiguration.WidgetConfigurationId == widgetConfigurationId).ToList();
            return result;
        }

        public IEnumerable<IWidgetType> GetWidgetTypes()
        {
            var result = getWidgetTypes().ToList();
            return result;
        }

        public IEnumerable<IObjectType> GetObjectTypes()
        {
            var result = getObjectTypes().ToList();
            return result;
        }

        public IEnumerable<IProperty> GetProperties()
        {
            var result = getProperties().ToList();
            return result;
        }

        public IEnumerable<IWidgetConfiguration> GetWidgetConfigurations()
        {
            var result = getWidgetConfigurations().ToList(); 
            return result;
        }


        private IEnumerable<IWidgetType> getWidgetTypes()
        {
            string name = System.Reflection.MethodBase.GetCurrentMethod().Name;
            List<IWidgetType> result = new List<IWidgetType>();
            string cacheKey = _appConfiguration["ResultSetCacheKey:WidgetTypes"].Value.ToString();

            if (!_memoryCache.TryGetValue(cacheKey, out result))
            {
                lock (CacheLockObject)
                {
                    IEnumerable<WidgetTypeResult> items = null;
                    using (DataRepository WidgetTypeRepository = new DataRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
                    {
                        items = WidgetTypeRepository.GetAll<WidgetTypeResult>(typeof(WidgetType), "Widget").ToList();
                    }

                    if (result == null)
                    {
                        result = new List<IWidgetType>();
                    }
                    items.ToList().ForEach(x =>
                    {
                        using (WidgetTypeBuilder builder = new WidgetTypeBuilder())
                        {
                            builder.Build(x);
                            result.Add(builder.GetResult() as WidgetType);
                        }
                    });

                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromHours(3));

                    _memoryCache.Set(cacheKey, result, cacheEntryOptions);
                }
            }

            return result;
        }

        public IEnumerable<IWidgetTypeWidgetConfiguration> GetEditableWidgetConfigurationsByWidgetType(int widgetTypeId)
        {
            IEnumerable<IWidgetTypeWidgetConfiguration> widgetTypeWidgetConfigurations = getWidgetTypeWidgetCongrigurations().Where(x => x.WidgetType.WidgetTypeId == widgetTypeId && x.WidgetConfiguration.IsEditable).ToList();
            return widgetTypeWidgetConfigurations;
        }

        private IEnumerable<IObjectType> getObjectTypes()
        {
            string name = System.Reflection.MethodBase.GetCurrentMethod().Name;
            List<IObjectType> result = new List<IObjectType>();
            string cacheKey = _appConfiguration["ResultSetCacheKey:ObjectTypes"].Value.ToString();

            if (!_memoryCache.TryGetValue(cacheKey, out result))
            {
                lock (CacheLockObject)
                {
                    IEnumerable<ObjectTypeResult> items = null;
                    using (DataRepository dataRepository = new DataRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
                    {
                        items = dataRepository.GetAll<ObjectTypeResult>(typeof(ObjectType), "Widget").ToList();
                    }

                    if (result == null)
                    {
                        result = new List<IObjectType>();
                    }
                    items.ToList().ForEach(x =>
                    {
                        using (ObjectTypeBuilder builder = new ObjectTypeBuilder())
                        {
                            builder.Build(x);
                            result.Add(builder.GetResult() as ObjectType);
                        }
                    });

                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromHours(3));

                    _memoryCache.Set(cacheKey, result, cacheEntryOptions);
                }
            }

            return result;
        }

        public dynamic GetWidgetConfigurationByWidgetType(int widgetTypeId, int dataSourceId)
        {
            var result = getWidgetTypeWidgetCongrigurations().Where(x => x.WidgetType.WidgetTypeId == widgetTypeId).ToList();

            var widgetType = GetWidgetTypeByWidgetTypeId(widgetTypeId);

            dynamic widgetObject = new ExpandoObject();


            foreach (var item in result.Where(x => x.Required).ToList())
            {
                switch (item.WidgetConfiguration.Name)
                {
                    //case "dataSource":
                    //    {
                    //        dynamic widgetObjectProperty = new ExpandoObject();
                    //        foreach (var property in item.WidgetConfiguration.WidgetConfigurationProperties)
                    //        {
                    //            dynamic propertyValue = property.DefaultValue;

                    //            switch (property.Property.Name)
                    //            {
                    //                case "data":
                    //                    {
                    //                        var dataSource = GetDataSourceData(dataSourceId);

                    //                        List<dynamic> dataSourceItems = new List<dynamic>();
                    //                        foreach (var dataItem in dataSource)
                    //                        {
                    //                            dynamic dataSourceItem = new ExpandoObject();
                    //                            foreach (var field in dataItem)
                    //                            {
                    //                                ExpandoHelper.AddProperty(dataSourceItem, field.Key, field.Value);
                    //                            }
                    //                            dataSourceItems.Add(dataSourceItem);
                    //                        }


                    //                        LinqHelper linqHelper = new LinqHelper();

                    //                        //Type linqHelperType = Type.GetType("JasonCarter.BudgetDashboard.Business.Helpers.LinqHelper");
                    //                        //object helper = Activator.CreateInstance(linqHelperType);

                    //                        string aggregateType = "Sum";




                    //                        var resultSet = dataSourceItems.Where(x => (decimal)((IDictionary<String, Object>)x)[seriesField]>150)
                    //                                                        //.GroupBy(x => linqHelper.GroupBy(((IDictionary<String, Object>)x)[categoryField],"MonthYear"))
                    //                                                        .GroupBy(x => (((IDictionary<String, Object>)x)[categoryField]))
                    //                                                        .Select(x => new Dictionary<string, object>()
                    //                                                        {
                    //                                                            { categoryField,  x.Key },
                    //                                                            //{ seriesField,  x.Sum(y => (double)y.Amount) },
                    //                                                            { seriesField,  linqHelper.GetType().GetMethod(aggregateType).Invoke(linqHelper,new object[] { x, seriesField })  }
                    //                                                        })
                    //                                                        //.OrderBy(x => ((IDictionary<String, Object>)x)[categoryField])
                    //                                                        .ToList();


                    //                        //var dataFiltered2 = ((IEnumerable)dataSource).Cast<dynamic>()
                    //                        //                                            .Where(x => x.Amount > 100)
                    //                        //                                            .GroupBy(x => x.TransactionSource)
                    //                        //                                            .Select(y => new
                    //                        //                                            {
                    //                        //                                                TransactionSource = y.Key,
                    //                        //                                                Amt = SumAmount(y, "Amount"),
                    //                        //                                                Amount = y.Sum(x => (double)x.Amount)
                    //                        //                                            })
                    //                        //                                            .OrderBy(x => x.TransactionSource)
                    //                        //                                            .ToList();

                    //                        propertyValue = resultSet;
                    //                        ExpandoHelper.AddProperty(widgetObjectProperty, property.Property.Name, propertyValue);
                    //                        break;
                    //                    }
                    //            }

                                
                    //        }
                    //        ExpandoHelper.AddProperty(widgetObject, item.WidgetConfiguration.Name, widgetObjectProperty);
                    //        break;
                    //    }


                    //case "categoryAxis":
                    //    {
                    //        dynamic widgetObjectProperty = new ExpandoObject();
                    //        foreach (var property in item.WidgetConfiguration.WidgetConfigurationProperties)
                    //        {
                    //            dynamic propertyValue = property.DefaultValue;

                    //            switch (property.Property.Name)
                    //            {
                    //                case "field":
                    //                    {
                    //                        propertyValue = categoryField;
                    //                        ExpandoHelper.AddProperty(widgetObjectProperty, property.Property.Name, propertyValue);
                    //                        break;
                    //                    }
                    //            }
                    //        }

                    //        ExpandoHelper.AddProperty(widgetObject, item.WidgetConfiguration.Name, widgetObjectProperty);
                    //        break;
                    //    }

                    //case "series":
                    //    {
                    //        dynamic widgetObjectProperty = new ExpandoObject();
                    //        foreach (var property in item.WidgetConfiguration.WidgetConfigurationProperties)
                    //        {
                    //            dynamic propertyValue = property.DefaultValue;

                    //            switch (property.Property.Name)
                    //            {
                                    
                    //                case "field":
                    //                    {
                    //                        propertyValue = seriesField;
                    //                        ExpandoHelper.AddProperty(widgetObjectProperty, property.Property.Name, propertyValue);
                    //                        break;
                    //                    }
                    //                case "name":
                    //                    {
                    //                        propertyValue = seriesField + " total";
                    //                        ExpandoHelper.AddProperty(widgetObjectProperty, property.Property.Name, propertyValue);
                    //                        break;
                    //                    }
                    //            }
                    //        }

                    //        dynamic[] hey = new dynamic[] { widgetObjectProperty };
                    //        ExpandoHelper.AddProperty(widgetObject, item.WidgetConfiguration.Name, hey);

                    //        break;
                    //    }


                    case "seriesDefaults":
                        {
                            dynamic widgetObjectProperty = new ExpandoObject();
                            foreach (var property in item.WidgetConfiguration.WidgetConfigurationProperties)
                            {
                                dynamic propertyValue = property.DefaultValue;

                                switch (property.Property.Name)
                                {
                                    case "type":
                                        {
                                            propertyValue = widgetType.Type;
                                            ExpandoHelper.AddProperty(widgetObjectProperty, property.Property.Name, propertyValue);
                                            break;
                                        }
                                }
                            }

                            ExpandoHelper.AddProperty(widgetObject, item.WidgetConfiguration.Name, widgetObjectProperty);
                            break;
                        }
                    default:
                        {
                            dynamic widgetObjectProperties = new ExpandoObject();
                            switch (item.WidgetConfiguration.ObjectType.Name)
                            {
                                case "array":
                                    {
                                        dynamic widgetObjectProperty = new ExpandoObject();
                                        foreach (var property in item.WidgetConfiguration.WidgetConfigurationProperties)
                                        {
                                            dynamic propertyValue = property.DefaultValue;

                                            switch (property.Property.Name)
                                            {
                                                //case "field":
                                                //    {
                                                //        propertyValue = "Amount";
                                                //        break;
                                                //    }
                                                //case "categoryField":
                                                //    {
                                                //        propertyValue = categoryField;
                                                //        break;
                                                //    }
                                                //case "name":
                                                //    {
                                                //        propertyValue = "Transaction Source Total";
                                                //        break;
                                                //    }
                                                //case "type":
                                                //    {
                                                //        propertyValue = item.WidgetType.Type;
                                                //        break;
                                                //    }
                                            }

                                            ExpandoHelper.AddProperty(widgetObjectProperty, property.Property.Name, propertyValue);
                                        }

                                        //dynamic[] hey = new dynamic[] { widgetObjectProperty };

                                        dynamic[] array = new dynamic[] { };

                                        ExpandoHelper.AddProperty(widgetObject, item.WidgetConfiguration.Name, array);
                                        break;
                                    }
                                case "object":
                                    {
                                        foreach (var property in item.WidgetConfiguration.WidgetConfigurationProperties)
                                        {
                                            dynamic propertyValue = property.DefaultValue;

                                            //switch (property.Property.Name)
                                            //{
                                            //    case "data":
                                            //        {
                                            //            //var dataSource = GetDataSourceData(dataSourceId);

                                            //            //List<dynamic> dataSourceItems = new List<dynamic>();
                                            //            //foreach (var dataItem in dataSource)
                                            //            //{
                                            //            //    dynamic dataSourceItem = new ExpandoObject();
                                            //            //    foreach (var field in dataItem)
                                            //            //    {

                                            //            //        ExpandoHelper.AddProperty(dataSourceItem, field.Key, field.Value);
                                            //            //    }
                                            //            //    dataSourceItems.Add(dataSourceItem);
                                            //            //}

                                            //            //// dataSource = (DataSource)resultClass.GetType().GetMethod("Convert").Invoke(resultClass, null);


                                            //            //LinqHelper linqHelper = new LinqHelper();


                                            //            //MethodInfo methodInfo2 = linqHelper.GetType().GetMethod("SumAmount");

                                            //            //// Handle to the Count method of System.Linq.Enumerable
                                            //            //MethodInfo countMethodInfo = typeof(System.Linq.Enumerable).GetMethod("Count", new Type[] { typeof(IEnumerable<>) });
                                            //            //MethodInfo sumMethodInfo = typeof(System.Linq.Enumerable).GetMethod("Sum", new Type[] { typeof(IEnumerable<>) });


                                            //            //MethodInfo methodInfo = this.GetType().GetMethod("SumAmount");


                                            //            //var resultSet = dataSourceItems.GroupBy(x => ((IDictionary<String, Object>)x)[categoryField])
                                            //            //                                .Select(x => new Dictionary<string, object>()
                                            //            //                                {
                                            //            //                                    { categoryField,  x.Key },
                                            //            //                                    { seriesField,  SumAmount(x, seriesField) }
                                            //            //                                })
                                            //            //                                .OrderBy(x => ((IDictionary<String, Object>)x)[categoryField])
                                            //            //                                .ToList();


                                            //            //var dataFiltered2 = ((IEnumerable)dataSource).Cast<dynamic>()
                                            //            //                                            .Where(x => x.Amount > 100)
                                            //            //                                            .GroupBy(x => x.TransactionSource)
                                            //            //                                            .Select(y => new
                                            //            //                                            {
                                            //            //                                                TransactionSource = y.Key,
                                            //            //                                                Amt = SumAmount(y, "Amount"),
                                            //            //                                                Amount = y.Sum(x => (double)x.Amount)
                                            //            //                                            })
                                            //            //                                            .OrderBy(x => x.TransactionSource)
                                            //            //                                            .ToList();

                                            //            //propertyValue = resultSet;
                                            //            break;
                                            //        }
                                            //    default:
                                            //        {
                                            //            propertyValue = property.DefaultValue;
                                            //            break;
                                            //        }
                                            //}



                                            ExpandoHelper.AddProperty(widgetObjectProperties, property.Property.Name, propertyValue);

                                        }
                                        ExpandoHelper.AddProperty(widgetObject, item.WidgetConfiguration.Name, widgetObjectProperties);
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }
                            break;
                        }
                }
            }

            string objectJson = JsonConvert.SerializeObject(widgetObject);


            dynamic data = GetDataSourceData(dataSourceId);

            IDictionary<string, object> returnValue = new Dictionary<string, object>();

            returnValue.Add("data", data);
            returnValue.Add("chartShellStart", "$('#divWidgetCanvas').kendoChart({");
            returnValue.Add("chartShell", "$('#divWidgetCanvas').kendoChart({ dataSource : {data:data}, series :[{name:'hey', field: 'Amount', categoryField: 'TransactionSource'}]});");
            returnValue.Add("chartObject", "$('#divWidgetCanvas').data('kendoChart')");
            returnValue.Add("configuration", widgetObject);


            return returnValue;
        }



       

      

        private static object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName).GetValue(obj, null);
        }

        private IEnumerable<IProperty> getProperties()
        {
            List<IProperty> result = new List<IProperty>();
            string cacheKey = _appConfiguration["ResultSetCacheKey:Properties"].Value.ToString();

            if (!_memoryCache.TryGetValue(cacheKey, out result))
            {
                lock (CacheLockObject)
                {
                    IEnumerable<PropertyResult> items = null;
                    using (DataRepository dataRepository = new DataRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
                    {
                        items = dataRepository.GetAll<PropertyResult>(typeof(Property), "Widget").ToList();
                    }

                    if (result == null)
                    {
                        result = new List<IProperty>();
                    }
                    items.ToList().ForEach(x =>
                    {
                        using (PropertyBuilder builder = new PropertyBuilder(this))
                        {
                            builder.Build(x);
                            result.Add(builder.GetResult() as Property);
                        }
                    });

                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromHours(3));

                    _memoryCache.Set(cacheKey, result, cacheEntryOptions);
                }
            }

            return result;
        }

        private IEnumerable<ISupportedValue> getSupportedValues()
        {
            List<ISupportedValue> result = new List<ISupportedValue>();
            string cacheKey = _appConfiguration["ResultSetCacheKey:SupportedValues"].Value.ToString();

            if (!_memoryCache.TryGetValue(cacheKey, out result))
            {
                lock (CacheLockObject)
                {
                    IEnumerable<SupportedValueResult> items = null;
                    using (DataRepository dataRepository = new DataRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
                    {
                        items = dataRepository.GetAll<SupportedValueResult>(typeof(SupportedValue), "Widget").ToList();
                    }

                    if (result == null)
                    {
                        result = new List<ISupportedValue>();
                    }
                    items.ToList().ForEach(x =>
                    {
                        using (SupportedValueBuilder builder = new SupportedValueBuilder())
                        {
                            builder.Build(x);
                            result.Add(builder.GetResult() as SupportedValue);
                        }
                    });

                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromHours(3));

                    _memoryCache.Set(cacheKey, result, cacheEntryOptions);
                }
            }

            return result;
        }


        private IEnumerable<IWidgetConfiguration> getWidgetConfigurationsLite()
        {
            List<IWidgetConfiguration> result = new List<IWidgetConfiguration>();
            string cacheKey = _appConfiguration["ResultSetCacheKey:WidgetConfigurationsLite"].Value.ToString();

            if (!_memoryCache.TryGetValue(cacheKey, out result))
            {
                lock (CacheLockObject)
                {
                    IEnumerable<WidgetConfigurationResult> items = null;
                    using (DataRepository dataRepository = new DataRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
                    {
                        items = dataRepository.GetAll<WidgetConfigurationResult>(typeof(WidgetConfiguration), "Widget").ToList();
                    }

                    if (result == null)
                    {
                        result = new List<IWidgetConfiguration>();
                    }
                    items.ToList().ForEach(x =>
                    {
                        using (WidgetConfigurationBuilder builder = new WidgetConfigurationBuilder(this))
                        {
                            builder.BuildLite(x);
                            result.Add(builder.GetResult() as WidgetConfiguration);
                        }
                    });

                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromHours(3));

                    _memoryCache.Set(cacheKey, result, cacheEntryOptions);
                }
            }

            return result;

        }


        private IEnumerable<IWidgetConfiguration> getWidgetConfigurations()
        {
            List<IWidgetConfiguration> result = new List<IWidgetConfiguration>();
            string cacheKey = _appConfiguration["ResultSetCacheKey:WidgetConfigurations"].Value.ToString();

            if (!_memoryCache.TryGetValue(cacheKey, out result))
            {
                lock (CacheLockObject)
                {
                    IEnumerable<WidgetConfigurationResult> items = null;
                    using (DataRepository dataRepository = new DataRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
                    {
                        items = dataRepository.GetAll<WidgetConfigurationResult>(typeof(WidgetConfiguration), "Widget").ToList();
                    }

                    if (result == null)
                    {
                        result = new List<IWidgetConfiguration>();
                    }
                    items.ToList().ForEach(x =>
                    {
                        using (WidgetConfigurationBuilder builder = new WidgetConfigurationBuilder(this))
                        {
                            builder.Build(x);
                            result.Add(builder.GetResult() as WidgetConfiguration);
                        }
                    });

                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromHours(3));

                    _memoryCache.Set(cacheKey, result, cacheEntryOptions);
                }
            }

            return result;
        }

        private IEnumerable<IWidgetConfigurationProperty> getWidgetConfigurationPropertiesLite()
        {
            List<IWidgetConfigurationProperty> result = new List<IWidgetConfigurationProperty>();
            string cacheKey = _appConfiguration["ResultSetCacheKey:WidgetConfigurationPropertiesLite"].Value.ToString();

            if (!_memoryCache.TryGetValue(cacheKey, out result))
            {
                lock (CacheLockObject)
                {
                    IEnumerable<WidgetConfigurationPropertyResult> items = null;
                    using (DataRepository dataRepository = new DataRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
                    {
                        items = dataRepository.GetAll<WidgetConfigurationPropertyResult>(typeof(WidgetConfigurationProperty), "Widget").ToList();
                    }

                    if (result == null)
                    {
                        result = new List<IWidgetConfigurationProperty>();
                    }
                    items.ToList().ForEach(x =>
                    {
                        using (WidgetConfigurationPropertyBuilder builder = new WidgetConfigurationPropertyBuilder())
                        {
                            builder.BuildLite(x);
                            result.Add(builder.GetResult() as WidgetConfigurationProperty);
                        }
                    });

                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromHours(3));

                    _memoryCache.Set(cacheKey, result, cacheEntryOptions);
                }
            }

            return result;
        }

        private IEnumerable<IWidgetConfigurationProperty> getWidgetConfigurationProperties()
        {
            List<IWidgetConfigurationProperty> result = new List<IWidgetConfigurationProperty>();
            string cacheKey = _appConfiguration["ResultSetCacheKey:WidgetConfigurationProperties"].Value.ToString();

            if (!_memoryCache.TryGetValue(cacheKey, out result))
            {
                lock (CacheLockObject)
                {
                    IEnumerable<WidgetConfigurationPropertyResult> items = null;
                    using (DataRepository dataRepository = new DataRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
                    {
                        items = dataRepository.GetAll<WidgetConfigurationPropertyResult>(typeof(WidgetConfigurationProperty), "Widget").ToList();
                    }

                    if (result == null)
                    {
                        result = new List<IWidgetConfigurationProperty>();
                    }
                    items.ToList().ForEach(x =>
                    {
                        using (WidgetConfigurationPropertyBuilder builder = new WidgetConfigurationPropertyBuilder(this))
                        {
                            builder.Build(x);
                            result.Add(builder.GetResult() as WidgetConfigurationProperty);
                        }
                    });

                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromHours(3));

                    _memoryCache.Set(cacheKey, result, cacheEntryOptions);
                }
            }

            return result;
        }

        public dynamic GetDataSourceDataByDataSourceIdAndDataFields(int dataSourceId, List<DataField> dataFields)
        {
            var dataSource = GetDataSourceData(dataSourceId);
            
            IList<string> groupByFields = new List<string>();
            IList<string> valueFields = new List<string>();

            foreach (var item in dataFields)
            {
                switch (item.type)
                {
                    case "categoryAxis":
                        {
                            groupByFields.Add(item.field);
                            break;
                        }
                    case "series":
                        {
                            valueFields.Add(item.field);
                            break;
                        }
                }
            }

            List<dynamic> dataSourceItems = new List<dynamic>();
            foreach (var dataItem in dataSource)
            {
                dynamic dataSourceItem = new ExpandoObject();
                foreach (var field in dataItem)
                {
                    if (field.Key == groupByFields[0])
                    {
                        ExpandoHelper.AddProperty(dataSourceItem, field.Key, field.Value);
                    }

                    foreach (var item in valueFields)
                    {
                        if (field.Key == item)
                        {
                            ExpandoHelper.AddProperty(dataSourceItem, field.Key, field.Value);
                        }
                    }
                }
                dataSourceItems.Add(dataSourceItem);
            }


            //dynamic resultSet = null;

            var resultSet = dataSourceItems.GroupBy(x => ((IDictionary<String, Object>)x)[groupByFields[0]]);
            //var hey = resultSet.Select(x => new Dictionary<string, object>()
            //                                                            {
            //                                                                { groupByFields[0],  x.Key }

            //                                                            })
            //.OrderBy(x => ((IDictionary<String, Object>)x)[groupByFields[0]]).ToList();

            if (dataFields.Any(x => x.field == groupByFields[0] && x.DataTransformType == "MonthYear"))
            {
                resultSet = dataSourceItems.GroupBy(x => Functions.GetMonthYear(Convert.ToDateTime(((IDictionary<String, Object>)x)[groupByFields[0]])));
                                           

                
            }
            else if (dataFields.Any(x => x.field == groupByFields[0] && x.DataTransformType == "Year"))
            {
                resultSet = dataSourceItems.GroupBy(x => Functions.GetYear(Convert.ToDateTime(((IDictionary<String, Object>)x)[groupByFields[0]])));
            }
            else
            {
                resultSet = dataSourceItems.GroupBy(x => ((IDictionary<String, Object>)x)[groupByFields[0]]);
            }


            dynamic resultValue = null;

            if (valueFields.Count() == 0)
            {
                var hey = resultSet.Select(x => new Dictionary<string, object>()
                                                                            {
                                                                                { groupByFields[0],  x.Key }
                                                                            })
                        .OrderBy(x => ((IDictionary<String, Object>)x)[groupByFields[0]]).ToList();

                resultValue = hey;
            }
            else
            {
                var hey = resultSet.Select(x => getGroupSelect(x, groupByFields.ToArray(), valueFields.ToArray()));

                //var hey = resultSet.Select(x => new Dictionary<string, object>()
                //                                                            {
                //                                                                { categoryFields[0],  x.Key },
                //                                                                { seriesFields[0],  linqHelper.GetType().GetMethod(aggregateType).Invoke(linqHelper,new object[] { x, seriesFields[0] })  }
                //                                                            })
                //.OrderBy(x => ((IDictionary<String, Object>)x)[categoryFields[0]]).ToList();
                resultValue = hey;
            }



            LinqHelper linqHelper = new LinqHelper();

            string aggregateType = "Sum";

          

            //if (seriesFields.Length == 0)
            //{
            //    var hey = resultSet.Select(x => new Dictionary<string, object>()
            //                                                                {
            //                                                                    { categoryFields[0],  x.Key }
            //                                                                })
            //            .OrderBy(x => ((IDictionary<String, Object>)x)[categoryFields[0]]).ToList();

            //    resultValue = hey;
            //}
            //else
            //{
            //    var hey = resultSet.Select(x => getGroupSelect(x, categoryFields, seriesFields));

            //    //var hey = resultSet.Select(x => new Dictionary<string, object>()
            //    //                                                            {
            //    //                                                                { categoryFields[0],  x.Key },
            //    //                                                                { seriesFields[0],  linqHelper.GetType().GetMethod(aggregateType).Invoke(linqHelper,new object[] { x, seriesFields[0] })  }
            //    //                                                            })
            //    //.OrderBy(x => ((IDictionary<String, Object>)x)[categoryFields[0]]).ToList();
            //    resultValue = hey;
            //}
            
            return resultValue;
        }

       

        private object getGroupSelect(IGrouping<object, dynamic> x, string[] categoryFields, string[] seriesFields)
        {
            var selects = new Dictionary<string, object>();

            LinqHelper linqHelper = new LinqHelper();

            string aggregateType = "Sum";

            selects.Add(categoryFields[0], x.Key);

            foreach(var item in seriesFields)
            {
                selects.Add(item, linqHelper.GetType().GetMethod(aggregateType).Invoke(linqHelper, new object[] { x, item }));
            }

            return selects;
        }

        public dynamic GetDataSourceData(int dataSourceId)
        {
            DataSource dataSource = GetDataSource(dataSourceId);

            dynamic items = null;

            using (DataRepository dataRepository = new DataRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
            {
                items = dataRepository.ExecuteStoredProcedure(dataSource.CommandText);
            }

            return items;
        }

        public DataSource GetDataSource(int dataSourceId)
        {
            DataSource result = new DataSource();
            DataSourceResult item = null;
            using (DataRepository dataRepository = new DataRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
            {
                item = dataRepository.GetById<DataSourceResult>(dataSourceId, typeof(DataSource));
            }

            using (DataSourceBuilder builder = new DataSourceBuilder())
            {
                builder.Build(item);
                result = builder.GetResult() as DataSource;
            }
        
            return result;
        }

        public DataSource GetDataSourceByDataSourceIdCategoryFieldSeriesFields(int dataSourceId, string categoryField)
        {
            DataSource result = new DataSource();
            DataSourceResult item = null;
            using (DataRepository dataRepository = new DataRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
            {
                item = dataRepository.GetById<DataSourceResult>(dataSourceId, typeof(DataSource));
            }

            using (DataSourceBuilder builder = new DataSourceBuilder())
            {
                builder.Build(item);
                result = builder.GetResult() as DataSource;
            }

            return result;
        }


      

        public IEnumerable<IDashboard> GetDashboards()
        {
            List<Dashboard> result = new List<Dashboard>();
            IEnumerable<DashboardResult> items = null;
            using (DataRepository dataRepository = new DataRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
            {
                items = dataRepository.GetAll<DashboardResult>(typeof(Dashboard), "Dashboard").ToList();
            }

            items.ToList().ForEach(x =>
            {
                using (DashboardBuilder builder = new DashboardBuilder())
                {
                    builder.Build(x);
                    result.Add(builder.GetResult() as Dashboard);
                }
            });

            return result;

        }

        public IEnumerable<IDataSource> GetDataSources()
        {
            List<DataSource> result = new List<DataSource>();
            IEnumerable<DataSourceResult> items = null;
            using (DataRepository dataRepository = new DataRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
            {
                items = dataRepository.GetAll<DataSourceResult>(typeof(DataSource)).ToList();
            }

            items.ToList().ForEach(x =>
            {
                using (DataSourceBuilder builder = new DataSourceBuilder())
                {
                    builder.Build(x);
                    result.Add(builder.GetResult() as DataSource);
                }
            });

            return result;
        }

        public IEnumerable<IWidgetTypeWidgetConfiguration> getWidgetTypeWidgetCongrigurations()
        {
            string name = System.Reflection.MethodBase.GetCurrentMethod().Name;
            List<IWidgetTypeWidgetConfiguration> result = new List<IWidgetTypeWidgetConfiguration>();
            string cacheKey = _appConfiguration["ResultSetCacheKey:WidgetTypeWidgetConfigurations"].Value.ToString();

            if (!_memoryCache.TryGetValue(cacheKey, out result))
            {
                lock (CacheLockObject)
                {
                    IEnumerable<WidgetTypeWidgetConfigurationResult> items = null;
                    using (DataRepository dataRepository = new DataRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
                    {
                        items = dataRepository.GetAll<WidgetTypeWidgetConfigurationResult>(typeof(WidgetTypeWidgetConfiguration), "Widget").ToList();
                    }

                    if (result == null)
                    {
                        result = new List<IWidgetTypeWidgetConfiguration>();
                    }
                    items.ToList().ForEach(x =>
                    {
                        using (WidgetTypeWidgetConfigurationBuilder builder = new WidgetTypeWidgetConfigurationBuilder(this))
                        {
                            builder.Build(x);
                            result.Add(builder.GetResult() as WidgetTypeWidgetConfiguration);
                        }
                    });

                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromHours(3));

                    _memoryCache.Set(cacheKey, result, cacheEntryOptions);
                }
            }

            return result;
        }

        public IEnumerable<IWidgetConfigurationPropertySupportedValue> getWidgetConfigurationPropertySupportedValues()
        {
            List<IWidgetConfigurationPropertySupportedValue> result = new List<IWidgetConfigurationPropertySupportedValue>();
            string cacheKey = _appConfiguration["ResultSetCacheKey:WidgetConfigurationPropertySupportedValues"].Value.ToString();

            if (!_memoryCache.TryGetValue(cacheKey, out result))
            {
                lock (CacheLockObject)
                {
                    IEnumerable<WidgetConfigurationPropertySupportedValueResult> items = null;
                    using (DataRepository dataRepository = new DataRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
                    {
                        items = dataRepository.GetAll<WidgetConfigurationPropertySupportedValueResult>(typeof(WidgetConfigurationPropertySupportedValue), "Widget").ToList();
                    }

                    if (result == null)
                    {
                        result = new List<IWidgetConfigurationPropertySupportedValue>();
                    }
                    items.ToList().ForEach(x =>
                    {
                        using (WidgetConfigurationPropertySupportedValueBuilder builder = new WidgetConfigurationPropertySupportedValueBuilder(this))
                        {
                            builder.Build(x);
                            result.Add(builder.GetResult() as WidgetConfigurationPropertySupportedValue);
                        }
                    });

                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromHours(3));

                    _memoryCache.Set(cacheKey, result, cacheEntryOptions);
                }
            }

            return result;
        }
    }
}


