using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

using jsk.goBudgetMe.Models;
using System.Text;

namespace jsk.goBudgetMe.Services
{
    public class Util : IUtil
    {
        public string UniqueId
        {
            get
            {
                var len = 25;
                char[] baseChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToCharArray();

                var sb = new StringBuilder(len);
                var _random = new Random();

                for (int i = 0; i < len; i++)
                    sb.Append(baseChars[_random.Next(baseChars.Length)]);

                return sb.ToString();
            }
        }
    }
}
