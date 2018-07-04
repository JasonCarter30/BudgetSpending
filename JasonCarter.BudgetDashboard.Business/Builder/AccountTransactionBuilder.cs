using JasonCarter.BudgetDashboard.Business.Entities;
using JasonCarter.BudgetDashboard.Business.Facades;
using System;

namespace JasonCarter.BudgetDashboard.Business.Builder
{
    internal class AccountTransactionBuilder : Builder, IDisposable
    {
        internal IAccountTransaction accountTransaction;
        private AccountTransactionFacade _accountTransactionFacade { get; set; }

        internal AccountTransactionBuilder(AccountTransactionFacade accountTransactionFacade)
        {
            _accountTransactionFacade = accountTransactionFacade;
        }

        public override void Build<T>(T resultClass)
        {
            accountTransaction = (AccountTransaction)resultClass.GetType().GetMethod("Convert").Invoke(resultClass, null);

            int transationTypeId = ((dynamic)(resultClass)).TransactionTypeId;
            int transationSourceId = ((dynamic)(resultClass)).TransactionSourceId;
            accountTransaction.TransactionType = _accountTransactionFacade.GetTransactionTypeByTransactionTypeId(transationTypeId);
            accountTransaction.TransactionSource = _accountTransactionFacade.GetTransactionSourceByTransactionSourceId(transationSourceId);
            accountTransaction.Debit = accountTransaction.TransactionType.Name == "Debit" ? accountTransaction.Amount :0 ;
            accountTransaction.Credit = accountTransaction.TransactionType.Name == "Credit" ? accountTransaction.Amount : 0;
        }

        public void Dispose()
        {
            accountTransaction = null;
        }

        public override object GetResult()
        {
            return accountTransaction;
        }
    }
}
