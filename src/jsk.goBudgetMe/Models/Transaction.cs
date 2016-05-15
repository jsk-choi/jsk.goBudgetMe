using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace jsk.goBudgetMe.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class Transaction {

        [Key]
        public int TransactionId { get; set; }
        public string UniqueId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionDesc { get; set; }
        public decimal Amount { get; set; }
        public bool Posted { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
