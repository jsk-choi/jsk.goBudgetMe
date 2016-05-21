using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using jsk.goBudgetMe.Controllers;
using jsk.goBudgetMe.Models;
using jsk.goBudgetMe.Services;

using Moq;
using Xunit;
using Xunit.Abstractions;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Http.Features.Authentication.Internal;
using Microsoft.AspNet.Http.Features.Authentication;

namespace jsk.goBudgetMe.Tests.Controllers
{
    public class AccountControllerTest
    {
        private readonly IServiceProvider serviceProvider;

        private IUtil _util;
        private IAccountService _accountService;
        private ITransactionService _transactionService;
        private UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _appCtx;

        public AccountControllerTest()
        {
            var services = new ServiceCollection();

            services.AddEntityFramework()
                .AddInMemoryDatabase()
                .AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase());

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IUtil, Util>();
            serviceProvider = services.BuildServiceProvider();

            _appCtx = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _util = serviceProvider.GetService<IUtil>();
            _accountService = serviceProvider.GetService<IAccountService>();
            _userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            //_transactionService = new TransactionService(_appCtx, _accountService, _util);
            _transactionService = serviceProvider.GetService<ITransactionService>();
        }

        [Fact]
        public void Transaction()
        {            
            var user = new ApplicationUser
            {
                Email = "test@test.test",
                UserName = "test@test.test"
            };

            var result = _userManager.CreateAsync(user).Result;
            var uid = _util.UniqueId;

            Assert.Equal(uid, uid);
            Assert.Equal(true, result.Succeeded);
            Assert.Equal("test@test.test", _appCtx.Users.FirstOrDefault().Email);

            Console.WriteLine("durr face");



            //var tran = new Transaction
            //{
            //    User = _appCtx.Users.FirstOrDefault(),
            //    TransactionDate = DateTime.Now,
            //    TransactionDesc = "test",
            //    Amount = 1.99m,
            //    UniqueId = _util.UniqueId
            //};

            //var oiwjef = _transactionService.AddUpdateAsync(tran);
            //Assert.Equal(null, oiwjef);



            //Assert.Equal("test", _transactionService.AddUpdateAsync(tran).Result.TransactionDesc);
            //Assert.Equal(1, _transactionService.GetAsync(DateTime.MinValue, DateTime.MaxValue).Result.Count());
        }
    }
}
