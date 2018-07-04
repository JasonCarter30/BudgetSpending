using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Entities
{
    public interface IDebitCreditAggregate
    {
        int Year { get; set; }
        string Month { get; set; }
        decimal DebitTotal { get; set; }
        decimal CreditTotal { get; set; }
    }
}
