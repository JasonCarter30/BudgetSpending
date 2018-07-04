using JasonCarter.BudgetDashboard.Business.Entities;
using JasonCarter.BudgetDashboard.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.DataEntities
{
    public class DebitCreditAggregateResult: IDapperResult<IDebitCreditAggregate>
    {
        public int Year { get; set; }
        public string Month { get; set; }
        public decimal DebitTotal { get; set; }
        public decimal CreditTotal { get; set; }

        public IDebitCreditAggregate Convert()
        {
            DebitCreditAggregate debitCreditAggregate = new DebitCreditAggregate()
            {
                Year = Year,
                Month = Month,
                DebitTotal = DebitTotal,
                CreditTotal = CreditTotal
            };

            return debitCreditAggregate;
        }
    }
}
