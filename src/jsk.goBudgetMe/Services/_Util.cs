using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

using jsk.goBudgetMe.Models;

namespace jsk.goBudgetMe.Services
{
    public class Util : IUtil
    {
        public string UniqueId
        {
            get
            {
                return Guid.NewGuid().ToString().Replace("-", "");
            }
        }
    }
}
