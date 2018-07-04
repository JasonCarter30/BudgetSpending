using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public class Property : IProperty
    {
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public IObjectType ObjectType { get; set; }
    }
}
