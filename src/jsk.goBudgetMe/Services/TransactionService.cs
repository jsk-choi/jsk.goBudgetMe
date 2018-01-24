using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using jsk.goBudgetMe.Models;
using Microsoft.Data.Entity;
using System.Data.SqlClient;

namespace jsk.goBudgetMe.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _appCtx;
        private readonly IAccountService _accountService;
        private readonly IUtil _util;

        public TransactionService(
            ApplicationDbContext appCtx,
            IAccountService accountService,
            IUtil util)
        {
            _appCtx = appCtx;
            _accountService = accountService;
            _util = util;
        }

        public async Task<IEnumerable<TransactionDto>> GetAsync(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var minDt = DateTime.MinValue;
                IEnumerable<TransactionDto> trans = new List<TransactionDto>();
                var User = _accountService.CurrentUser;

                if (User == null) return null;

                var sql = @"exec spGetTransaction @userId = {0}, @startDate = {1}, @endDate = {2};";
                if (startDate != minDt && endDate != minDt)
                {
                    trans = _appCtx.TransactionDtos.FromSql(sql,
                        _accountService.CurrentUser.Id,
                        startDate,
                        endDate);
                }
                else if (startDate != minDt)
                {
                    trans = _appCtx.TransactionDtos.FromSql(sql,
                        _accountService.CurrentUser.Id,
                        startDate,
                        DateTime.MaxValue);
                }
                else if (endDate != minDt)
                {
                    trans = _appCtx.TransactionDtos.FromSql(sql,
                        _accountService.CurrentUser.Id,
                        DateTime.MinValue.AddYears(1900),
                        endDate);
                }
                else {
                    trans = _appCtx.TransactionDtos.FromSql(sql,
                        _accountService.CurrentUser.Id,
                        DateTime.MinValue.AddYears(1900),
                        DateTime.MaxValue);
                }
                return await Task.Run(() => trans.OrderBy(x => x.TransactionDate));
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
                    );
                return await Task.Run(() => tran.FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Transaction> GetItemAsync(int TransactionId)
        {
            try
            {
                var tran = _appCtx.Transactions.
                    Where(x =>
                        x.User.Id == _accountService.CurrentUser.Id &&
                        x.TransactionId == TransactionId
                    );
                return await Task.Run(() => tran.FirstOrDefault());
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
                if (_appCtx.Transactions.Where(x => x.UniqueId == Transaction.UniqueId).Any())
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
