using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public interface IObjectType
    {
        int ObjectTypeId { get; set; }
        string Name { get; set; }
    }
}
