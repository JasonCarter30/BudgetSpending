using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Builder
{
    public interface IEntityBuilder<out T> : IDisposable
    {
        void Build();
        T GetResult();
    }
}
