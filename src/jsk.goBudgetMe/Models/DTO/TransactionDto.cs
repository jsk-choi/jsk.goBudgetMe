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
    public class TransactionDto {

        [Key]
        public int TransactionId { get; set; }

        public string UniqueId { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime TransactionDate { get; set; }

        [Required]
        [Display(Name = "Transaction")]
        [StringLength(100, ErrorMessage = "A description is required.", MinimumLength = 1)]
        public string TransactionDesc { get; set; }

        [Required]
        [Range(-100000000, 100000000)]
        public decimal Amount { get; set; }

        public bool Posted { get; set; }

        public decimal Balance { get; set; }
    }
}
