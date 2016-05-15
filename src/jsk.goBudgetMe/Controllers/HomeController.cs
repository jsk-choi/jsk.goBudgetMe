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

namespace jsk.goBudgetMe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _appCtx;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public HomeController(
            ApplicationDbContext appCtx, 
            UserManager<ApplicationUser> userManager,
            ITransactionService transactionService,
            IMapper mapper)
        {
            _appCtx = appCtx;
            _userManager = userManager;
            _transactionService = transactionService;
            _mapper = mapper;
        }

        public JsonResult Durr()
        {
            var oooop = User.Identity.Name;
            var myUser = _userManager.FindByNameAsync(oooop).Result;

            var sDate = DateTime.Now.AddDays(-100);
            var eDate = DateTime.Now.AddDays(100);
            var trans = _transactionService.GetAsync(startDate: sDate, endDate: eDate);

            var viewModel = _mapper.Map<IEnumerable<TransactionDto>>(trans.Result);
            return new JsonResult(viewModel);
        }

        public IActionResult Index()
        {
            return View();
        }


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
