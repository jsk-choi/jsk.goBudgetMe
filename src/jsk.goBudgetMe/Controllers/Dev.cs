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
    public class DevController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
