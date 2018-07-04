using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Entities
{
    public class DebitCreditAggregate : IDebitCreditAggregate
    {
        public int Year { get; set; }
        public string Month { get; set; }
        public decimal DebitTotal { get; set; }
        public decimal CreditTotal { get; set; }
    }
}
