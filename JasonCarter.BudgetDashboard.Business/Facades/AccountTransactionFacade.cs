using JasonCarter.BudgetDashboard.Business.Builder;
using JasonCarter.BudgetDashboard.Business.DataEntities;
using JasonCarter.BudgetDashboard.Business.Entities;
using JasonCarter.BudgetDashboard.Common;
using JasonCarter.BudgetDashboard.Data.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace JasonCarter.BudgetDashboard.Business.Facades
{
    public class AccountTransactionFacade
    {
        private AppConfiguration _appConfiguration;
        private IMemoryCache _memoryCache;
        private static readonly object CacheLockObject = new object();

        public AccountTransactionFacade(AppConfiguration appConfiguration, IMemoryCache memoryCache)
        {
            _appConfiguration = appConfiguration;
            _memoryCache = memoryCache;
        }

        public IEnumerable<IAccountTransaction> GetAccountTransactions()
        {
            var result = getAccountTransactions();

            clearMemoryCache();

            return result;
        }

        public IEnumerable<IAccountTransaction> GetAccountTransactionsOrderByDateDescending()
        {
            var result = getAccountTransactions().OrderByDescending(x => x.Date);
            return result;
        }

        public IEnumerable<IAccountTransaction> GetCurrentMonthAccountTransactionsOrderByDateDescending()
        {
            var result = getAccountTransactions().Where(x => x.Date.Month == DateTime.Now.Month).OrderByDescending(x => x.Date);
            return result;
        }

        public dynamic GetCurrentMonthAccountTransactions()
        {
            dynamic items = null;
            using (AccountTransactionRepository accountTransactionRepository = new AccountTransactionRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
            {
                IDictionary<string, object> parameters = new Dictionary<string, object>();

                parameters.Add("Year", DateTime.Today.Year);
                parameters.Add("Month", DateTime.Today.Month);

                items = accountTransactionRepository.ExecuteStoredProcedureCommand(_appConfiguration["GetAccountTransactionsPerYearMonthCommandText"].Value.ToString(), parameters);
            }

            return items;
        }

        public ITransactionType GetTransactionTypeByTransactionTypeId(int transactionTypeId)
        {
            var result = getTransactionTypes().Where(x => x.TransactionTypeId == transactionTypeId).FirstOrDefault();
            return result;
        }


        public ITransactionSource GetTransactionSourceByTransactionSourceId(int transactionSourceId)
        {
            var result = getTransactionSources().Where(x => x.TransactionSourceId == transactionSourceId).FirstOrDefault();
            return result;
        }

        private void clearMemoryCache()
        {
            _appConfiguration.Items.Where(x => x.Name.StartsWith("ResultSetCacheKey:")).ToList().ForEach(x =>
            {
                _memoryCache.Remove(x.Value.ToString());
            });
        }

        public IEnumerable<ITransactionType> GetTransactionTypes()
        {
            var result = getTransactionTypes();
            return result;
        }

        public void InsertAccountTransaction(InsertAccountTransactionPayload payloadData)
        {
            AccountTransactionResult accountTransactionResult = new AccountTransactionResult();

            accountTransactionResult.Date = payloadData.Date;
            accountTransactionResult.Amount = payloadData.Amount;
            accountTransactionResult.TransactionTypeId = payloadData.TransactionTypeId;
            accountTransactionResult.TransactionSourceId = payloadData.TransactionSourceId;
            accountTransactionResult.Notes = payloadData.Note;


            TransactionSourceResult transactionSourceResult = new TransactionSourceResult();
            transactionSourceResult.Name = payloadData.TransactionSourceName;
            using (AccountTransactionRepository accountTransactionRepository = new AccountTransactionRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
            {
                try
                {
                    accountTransactionRepository.BeginTransaction();

                    if (accountTransactionResult.TransactionSourceId == 0)
                    {
                        accountTransactionResult.TransactionSourceId = accountTransactionRepository.Insert<TransactionSourceResult>(typeof(TransactionSource), transactionSourceResult);
                    }

                    accountTransactionRepository.Insert(typeof(AccountTransaction), accountTransactionResult);

                    accountTransactionRepository.CommitTransaction();

                    clearMemoryCache();
                }
                catch (Exception)
                {
                    accountTransactionRepository.RollbackTransaction();
                    throw;
                }
            }

        }



        public void UpdateAccountTransaction(UpdateAccountTransactionPayload payloadData)
        {
            AccountTransactionResult accountTransactionResult = new AccountTransactionResult();

            accountTransactionResult.AccountTransactionId = payloadData.AccountTransactionId;
            accountTransactionResult.Date = payloadData.Date;
            accountTransactionResult.Amount = payloadData.Amount;
            accountTransactionResult.TransactionTypeId = payloadData.TransactionTypeId;
            accountTransactionResult.TransactionSourceId = payloadData.TransactionSourceId;
            accountTransactionResult.Notes = payloadData.Note;


            TransactionSourceResult transactionSourceResult = new TransactionSourceResult();
            transactionSourceResult.Name = payloadData.TransactionSourceName;
            using (AccountTransactionRepository accountTransactionRepository = new AccountTransactionRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
            {
                try
                {
                    accountTransactionRepository.BeginTransaction();

                    if (accountTransactionResult.TransactionSourceId == 0)
                    {
                        accountTransactionResult.TransactionSourceId = accountTransactionRepository.Insert<TransactionSourceResult>(typeof(TransactionSource), transactionSourceResult);
                    }

                    accountTransactionRepository.Update<AccountTransactionResult>(typeof(AccountTransaction), accountTransactionResult);

                    accountTransactionRepository.CommitTransaction();

                    clearMemoryCache();
                }
                catch (Exception)
                {
                    accountTransactionRepository.RollbackTransaction();
                    throw;
                }
            }

        }

        public object GetGasSpendingDetailsByYearAndMonth(int year, int month)
        {
            dynamic items = null;
            using (AccountTransactionRepository accountTransactionRepository = new AccountTransactionRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
            {
                IDictionary<string, object> parameters = new Dictionary<string, object>();

                parameters.Add("Year", year);
                parameters.Add("Month", month);

                items = accountTransactionRepository.ExecuteStoredProcedureCommand(_appConfiguration["GetGasSpendingDetailsByYearAndMonthCommandText"].Value.ToString(), parameters);
            }

            return items;
        }

        public IEnumerable<IAccountTransaction> GetTransactionsByTransactionSourceIdTransactionTypeIdMonthYear(int transactionSourceId, int transactionTypeId, int month, int year)
        {
            var result = getAccountTransactions().Where(x => x.TransactionSource.TransactionSourceId == transactionSourceId && x.TransactionType.TransactionTypeId == transactionTypeId && x.Date.Month == month && x.Date.Year == year).ToList();
            return result;
        }

        public IAccountTransaction GetAccountTransactionByAccountTransactionId(int accountTransactionId)
        {
            var result = getAccountTransactions().Where(x => x.AccountTransactionId == accountTransactionId).FirstOrDefault();
            return result;
        }

        public dynamic GetDebitCreditTotalsGroupByMonthByYear()
        {
            var result = getDebitCreditTotalsGroupByMonthByYear();
            return result;
        }


        public dynamic GetMonthlyGasSummaryByYear(int year)
        {
            dynamic items = null;
            using (AccountTransactionRepository accountTransactionRepository = new AccountTransactionRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
            {
                IDictionary<string, object> parameters = new Dictionary<string, object>();

                parameters.Add("year", year);

                items = accountTransactionRepository.ExecuteStoredProcedureCommand(_appConfiguration["GetMonthlyGasSummaryByYearCommandText"].Value.ToString(), parameters);
            }

            return items;
        }

        public dynamic GetRollingTwelveMonthGasSummary()
        {
            dynamic items = null;
            using (AccountTransactionRepository accountTransactionRepository = new AccountTransactionRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
            {
                IDictionary<string, object> parameters = new Dictionary<string, object>();

                items = accountTransactionRepository.ExecuteStoredProcedureCommand(_appConfiguration["GetMonthlyGasSummaryByYearCommandText"].Value.ToString(), parameters);
            }

            return items;
        }


        public dynamic GetYearlyTransactionSourceSummary(int year)
        {
            dynamic items = null;
            using (AccountTransactionRepository accountTransactionRepository = new AccountTransactionRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
            {
                IDictionary<string, object> parameters = new Dictionary<string, object>();

                parameters.Add("year", year);
                
                items = accountTransactionRepository.ExecuteStoredProcedureCommand(_appConfiguration["GetYearlyTransactionSourceSummaryCommandText"].Value.ToString(), parameters);
            }

            return items;
        }

        public object GetTransactionSources(string lookupValue)
        {
            var result = getTransactionSources().Where(x => x.Name.Contains(lookupValue));
            return result;
        }

        private IEnumerable<IAccountTransaction> getAccountTransactions()
        {
            string name = System.Reflection.MethodBase.GetCurrentMethod().Name;
            List<IAccountTransaction> result = new List<IAccountTransaction>();
            string cacheKey = _appConfiguration["ResultSetCacheKey:AccountTransactions"].Value.ToString();

            if (!_memoryCache.TryGetValue(cacheKey, out result))
            {
                lock (CacheLockObject)
                {
                    IEnumerable<AccountTransactionResult> items = null;
                    using(AccountTransactionRepository accountTransactionRepository = new AccountTransactionRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
                    {
                        items = accountTransactionRepository.GetAll<AccountTransactionResult>(typeof(AccountTransaction)).ToList();
                    }


                    if (result == null)
                    {
                        result = new List<IAccountTransaction>();
                    }
                    items.ToList().ForEach(x =>
                    {
                        using (AccountTransactionBuilder builder = new AccountTransactionBuilder(this))
                        {
                            builder.Build(x);
                            result.Add(builder.GetResult() as AccountTransaction);
                        }
                    });
                    
                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromHours(3));

                    _memoryCache.Set(cacheKey, result, cacheEntryOptions);
                }
            }
        

            return result;
        }

        private IEnumerable<ITransactionType> getTransactionTypes()
        {
            string name = System.Reflection.MethodBase.GetCurrentMethod().Name;
            List<ITransactionType> result = new List<ITransactionType>();
            string cacheKey = _appConfiguration["ResultSetCacheKey:TransactionTypes"].Value.ToString();

            if (!_memoryCache.TryGetValue(cacheKey, out result))
            {
                lock (CacheLockObject)
                {
                    IEnumerable<TransactionTypeResult> items = null;
                    using (AccountTransactionRepository accountTransactionRepository = new AccountTransactionRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
                    {
                        items = accountTransactionRepository.GetAll<TransactionTypeResult>(typeof(TransactionType)).ToList();
                    }


                    if (result == null)
                    {
                        result = new List<ITransactionType>();
                    }
                    items.ToList().ForEach(x =>
                    {
                        using (TransactionTypeBuilder builder = new TransactionTypeBuilder())
                        {
                            builder.Build(x);
                            result.Add(builder.GetResult() as TransactionType);
                        }
                    });

                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromHours(3));

                    _memoryCache.Set(cacheKey, result, cacheEntryOptions);
                }
            }


            return result;
        }


        private IEnumerable<ITransactionSource> getTransactionSources()
        {
            List<ITransactionSource> result = new List<ITransactionSource>();
            string cacheKey = _appConfiguration["ResultSetCacheKey:TransactionSources"].Value.ToString();

            if (!_memoryCache.TryGetValue(cacheKey, out result))
            {
                lock (CacheLockObject)
                {
                    IEnumerable<TransactionSourceResult> items = null;
                    using (AccountTransactionRepository accountTransactionRepository = new AccountTransactionRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
                    {
                        items = accountTransactionRepository.GetAll<TransactionSourceResult>(typeof(TransactionSource)).ToList();
                    }


                    if (result == null)
                    {
                        result = new List<ITransactionSource>();
                    }
                    items.ToList().ForEach(x =>
                    {
                        using (TransactionSourceBuilder builder = new TransactionSourceBuilder())
                        {
                            builder.Build(x);
                            result.Add(builder.GetResult() as TransactionSource);
                        }
                    });

                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromHours(3));

                    _memoryCache.Set(cacheKey, result, cacheEntryOptions);
                }
            }


            return result;
        }


        private dynamic getDebitCreditTotalsGroupByMonthByYear()
        {
                    IDictionary<string, object> parameters = new Dictionary<string, object>();


                    dynamic items = null;
                    using (AccountTransactionRepository accountTransactionRepository = new AccountTransactionRepository(_appConfiguration["DatabaseConnectionString"].Value.ToString()))
                    {
                        items = accountTransactionRepository.ExecuteStoredProcedureCommand(_appConfiguration["GetDebitCreaditTotalsGroupByMonthByYearCommandText"].Value.ToString(), parameters);
                    }



            return items;
        }
    }
}
