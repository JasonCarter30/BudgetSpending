using JasonCarter.BudgetDashboard.Business.Entities;
using JasonCarter.BudgetDashboard.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.DataEntities
{
    public class TransactionTypeResult:IDapperResult<ITransactionType>
    {
        public int TransactionTypeId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }

        public ITransactionType Convert()
        {
            ITransactionType transactionType = new TransactionType()
            {
                TransactionTypeId = TransactionTypeId,
                Name = Name,
                Title = Title,
                Description = Description,
                IsDefault = IsDefault
            };

            return transactionType;
        }
    }
}
