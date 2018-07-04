using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using JasonCarter.BudgetDashboard.Data.DataAccess;

namespace JasonCarter.BudgetDashboard.Business.Admin.DataEntities
{
    public class WidgetConfigurationResult : IDapperResult<IWidgetConfiguration>
    {
        public int WidgetConfigurationId { get; set; }
        public string Name { get; set; }
        public int ObjectTypeId { get; set; }
        public bool IsEditable { get; set; }
        public bool IsDataConfig { get; set; }

        public IWidgetConfiguration Convert()
        {
            IWidgetConfiguration widgetConfiguration = new WidgetConfiguration()
            {
                WidgetConfigurationId = WidgetConfigurationId,
                Name = Name,
                IsEditable = IsEditable,
                IsDataConfig = IsDataConfig
            };

            return widgetConfiguration;
        }
    }
}
