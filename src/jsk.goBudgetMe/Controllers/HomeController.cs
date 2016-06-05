using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;

using System.Security.Claims;

using Microsoft.AspNet.Http;
using jsk.goBudgetMe.Models;
using jsk.goBudgetMe.Services;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

using AutoMapper;
using Microsoft.AspNet.Authorization;

namespace jsk.goBudgetMe.Controllers
{

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _appCtx;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public HomeController(
            ApplicationDbContext appCtx,
            UserManager<ApplicationUser> userManager,
            ITransactionService transactionService,
            IMapper mapper,
            IAccountService accountService)
        {
            _appCtx = appCtx;
            _userManager = userManager;
            _transactionService = transactionService;
            _mapper = mapper;
            _accountService = accountService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var modelView = new TransactionView {
                dateStart = DateTime.Now.AddDays(-10),
                dateEnd = DateTime.Now.AddMonths(1)
            };

            var tran = await _transactionService.GetAsync(modelView.dateStart, modelView.dateEnd);

            modelView.Transactions = tran.ToList();

            return View(modelView);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index(TransactionView Transaction)
        {
            var tran = await _transactionService.GetAsync(Transaction.dateStart, Transaction.dateEnd);
            var modelView = new TransactionView
            {
                dateStart = Transaction.dateStart,
                dateEnd = Transaction.dateEnd,
                Transactions = tran.ToList()
            };
            return View(modelView);
        }

        #region EDIT
        [Authorize]
        [Route("edit")]
        public IActionResult Edit()
        {
            return RedirectToAction("Index");
        }

        [Authorize]
        [Route("edit/{uniqueId}")]
        public async Task<ActionResult> Edit(string uniqueId)
        {
            var tran = await _transactionService.GetItemAsync(uniqueId);

            if (tran == null)
                return RedirectToAction(
                    nameof(HomeController.Index),
                    nameof(HomeController).Replace("Controller", ""));

            var tranDto = _mapper.Map<TransactionDto>(tran);
            return View(tranDto);
        }

        [Authorize]
        [HttpPost]
        [Route("edit/{uniqueId}")]
        public async Task<ActionResult> Edit(TransactionDto transaction)
        {
            if (!ModelState.IsValid)
                return HttpBadRequest(ModelState);

            var tran = _mapper.Map<Transaction>(transaction);
            tran = await _transactionService.AddUpdateAsync(tran);

            return RedirectToAction(
                nameof(HomeController.Index),
                nameof(HomeController).Replace("Controller", ""));
        }
        #endregion

        #region ADD
        [Authorize]
        [Route("add")]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add(TransactionDto transaction)
        {
            var tran = _mapper.Map<Transaction>(transaction);
            tran = await _transactionService.AddUpdateAsync(tran);
            return RedirectToAction(
                nameof(HomeController.Index),
                nameof(HomeController).Replace("Controller", ""));
        }
        #endregion

        #region DELETE
        [Authorize]
        [Route("delete")]
        public ActionResult Delete()
        {
            return RedirectToAction(
                nameof(HomeController.Index),
                nameof(HomeController).Replace("Controller", ""));
        }

        [Authorize]
        [Route("delete/{uniqueId}")]
        public async Task<ActionResult> Delete(string uniqueId)
        {
            var tran = await _transactionService.DeleteAsync(uniqueId);
            return RedirectToAction(
                nameof(HomeController.Index),
                nameof(HomeController).Replace("Controller", ""));
        }
        #endregion

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
