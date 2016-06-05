using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace jsk.goBudgetMe.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class TransactionView
    {
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
        public IList<TransactionDto> Transactions { get; set; }
    }
}
