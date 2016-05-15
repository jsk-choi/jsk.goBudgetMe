using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using jsk.goBudgetMe.Models;

namespace jsk.goBudgetMe.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _appCtx;
        private readonly IAccountService _accountService;
        private readonly IUtil _util;

        public TransactionService(ApplicationDbContext appCtx, IAccountService accountService, IUtil util)
        {
            _appCtx = appCtx;
            _accountService = accountService;
            _util = util;
        }

        public async Task<IEnumerable<Transaction>> GetAsync(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var minDt = DateTime.MinValue;
                IEnumerable<Transaction> trans;
                var User = _accountService.CurrentUser;

                if (startDate != minDt && endDate != minDt)
                {
                    trans = _appCtx.Transactions.Where(
                        x => x.User.Id == User.Id &&
                        (x.TransactionDate >= startDate && x.TransactionDate <= endDate)
                    );
                }
                else if (startDate != minDt)
                {
                    trans = _appCtx.Transactions.Where(
                        x => x.User.Id == User.Id &&
                        (x.TransactionDate >= startDate)
                    );
                }
                else if (endDate != minDt)
                {
                    trans = _appCtx.Transactions.Where(
                        x => x.User.Id == User.Id &&
                        (x.TransactionDate <= endDate)
                    );
                }
                else {
                    trans = _appCtx.Transactions.Where(
                        x => x.User.Id == User.Id
                    );
                }
                return await Task.Run(() => trans);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Transaction> GetItemAsync(string UniqueId) {
            try
            {
                var tran = _appCtx.Transactions.
                    Where(x => 
                        x.User.Id == _accountService.CurrentUser.Id && 
                        x.UniqueId == UniqueId
                    ).FirstOrDefault();
                return await Task.Run(() => tran);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Transaction> AddUpdateAsync(Transaction Transaction)
        {
            try
            {
                Transaction.User = _accountService.CurrentUser;

                if (_appCtx.Transactions.Any(x => x.UniqueId == Transaction.UniqueId))
                {
                    _appCtx.Transactions.Update(Transaction);
                }
                else
                {
                    Transaction.UniqueId = _util.UniqueId;
                    _appCtx.Transactions.Add(Transaction);
                }
                _appCtx.SaveChanges();

                return await Task.Run(() => Transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Transaction> DeleteAsync(string UniqueId)
        {
            try
            {
                var tran = await GetItemAsync(UniqueId);
                _appCtx.Transactions.Remove(tran);
                _appCtx.SaveChanges();
                return await Task.Run(() => tran);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
