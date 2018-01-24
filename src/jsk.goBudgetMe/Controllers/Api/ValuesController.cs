using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

using jsk.goBudgetMe.Models;
using jsk.goBudgetMe.Services;

using AutoMapper;

namespace jsk.goBudgetMe.Controllers
{
    [Produces("application/json")]
    [Route("api/Values")]
    public class ValuesController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private ITransactionService _transactionService;
        private IAccountService _AccountService;
        private readonly IMapper _mapper;

        public ValuesController(
            UserManager<ApplicationUser> userManager,
            ITransactionService transactionService,
            IAccountService accountService,
            IMapper mapper)
        {
            _userManager = userManager;
            _transactionService = transactionService;
            _AccountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions(DateTime startDate, DateTime endDate)
        {
            if (!ModelState.IsValid)
                return HttpBadRequest(ModelState);

            try
            {
                var trans = await _transactionService.GetAsync(startDate, endDate);

                if (trans == null) return HttpNotFound();

                var transDto = _mapper.Map<IEnumerable<TransactionDto>>(trans);

                return Ok(transDto);
            }
            catch (Exception ex)
            {
                return HttpBadRequest(ex);
            }
        }

        [HttpGet("{UniqueId}", Name = "GetTransaction")]
        public async Task<IActionResult> GetTransaction([FromRoute] string UniqueId)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            try
            {
                var tran = await _transactionService.GetItemAsync(UniqueId);

                if (tran == null)
                    return new HttpNotFoundResult();

                var tranDto = _mapper.Map<TransactionDto>(tran);
                return Ok(tranDto);
            }
            catch (Exception ex)
            {
                return HttpBadRequest(ex);
            }
        }

        //// PUT: api/Values/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTransaction([FromRoute] int id, [FromBody] Transaction transaction)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return HttpBadRequest(ModelState);
        //    }

        //    if (id != transaction.TransactionId)
        //    {
        //        return HttpBadRequest();
        //    }

        //    _context.Entry(transaction).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TransactionExists(id))
        //        {
        //            return HttpNotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return new HttpStatusCodeResult(StatusCodes.Status204NoContent);
        //}

        // POST: api/Values

        [HttpPost]
        public async Task<IActionResult> PostTransaction([FromBody] TransactionDto Transaction)
        {
            if (!ModelState.IsValid)
                return HttpBadRequest(ModelState);

            try
            {
                var tran = _mapper.Map<Transaction>(Transaction);
                tran = await _transactionService.AddUpdateAsync(tran);
                var tranDto = _mapper.Map<TransactionDto>(tran);

                return Ok(tranDto);
            }
            catch (Exception ex)
            {
                return HttpBadRequest(ex);
            }
        }

        // DELETE api/values/{UniqueId}
        [HttpDelete("{UniqueId}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] string UniqueId)
        {
            if (!ModelState.IsValid)
                return HttpBadRequest(ModelState);

            var tran = await _transactionService.DeleteAsync(UniqueId);

            if (tran == null)
                return HttpNotFound();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        //private bool TransactionExists(int id)
        //{
        //    return _context.Transactions.Count(e => e.TransactionId == id) > 0;
        //}
    }

    public class CommentModel
    {
        public string Author { get; set; }
        public string Text { get; set; }
    }
}

