using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using jsk.goBudgetMe.Models;

namespace jsk.goBudgetMe.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAsync(DateTime? startDate, DateTime? endDate);
        Task<Transaction> GetItemAsync(string UniqueId);
        Task<Transaction> DeleteAsync(string UniqueId);
        Task<Transaction> AddUpdateAsync(Transaction Transaction);
    }
}
