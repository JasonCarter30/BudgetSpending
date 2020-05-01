using JasonCarter.BudgetDashboard.Business.DataEntities;
using JasonCarter.BudgetDashboard.Business.Entities;
using JasonCarter.BudgetDashboard.Business.Facades;
using System;

namespace JasonCarter.BudgetDashboard.Business.Builder
{
    internal class AccountTransactionBuilder : IEntityBuilder<IAccountTransaction>
    {
        private IAccountTransaction _accountTransaction;
        private AccountTransactionResult _accountTransactionResult;
        private AccountTransactionFacade _accountTransactionFacade { get; set; }

        internal AccountTransactionBuilder(AccountTransactionResult accountTransactionResult, AccountTransactionFacade accountTransactionFacade)
        {
            _accountTransactionFacade = accountTransactionFacade;
            _accountTransactionResult = accountTransactionResult;
        }

        public void Build()
        {
            _accountTransaction = new AccountTransaction();
            _accountTransaction.AccountTransactionId = _accountTransactionResult.AccountTransactionId;
            _accountTransaction.TransactionSourceId = _accountTransactionResult.TransactionSourceId;
            _accountTransaction.Date = _accountTransactionResult.Date;
            _accountTransaction.Amount = _accountTransactionResult.Amount;
            _accountTransaction.Notes = _accountTransactionResult.Notes;

            int transationTypeId = _accountTransactionResult.TransactionTypeId;
            int transationSourceId = _accountTransactionResult.TransactionSourceId;
            _accountTransaction.TransactionType = _accountTransactionFacade.GetTransactionTypeByTransactionTypeId(transationTypeId);
            _accountTransaction.TransactionSource = _accountTransactionFacade.GetTransactionSourceByTransactionSourceId(transationSourceId);
            _accountTransaction.Debit = _accountTransaction.TransactionType.Name == "Debit" ? _accountTransaction.Amount : 0;
            _accountTransaction.Credit = _accountTransaction.TransactionType.Name == "Credit" ? _accountTransaction.Amount : 0;
        }

        public IAccountTransaction GetResult()
        {
            return _accountTransaction;
        }

        public void Dispose()
        {
            _accountTransaction = null;
        }
    }
}