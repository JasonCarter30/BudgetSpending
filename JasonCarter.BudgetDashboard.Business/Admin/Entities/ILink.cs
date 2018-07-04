using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public interface ILink
    {
        int LinkId { get; set; }
        string Name { get; set; }
        string Title { get; set; }
        string URL { get; set; }
        bool Active { get; set; }
    }
}
