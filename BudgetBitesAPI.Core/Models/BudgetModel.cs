using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetBitesAPI.Core.Models
{
    public class BudgetModel
    {
        [Key]
        public int BudgetID { get; set; }
        public int PersonID { get; set; } = 0;
        public string? Username { get; set; }
        public DateTime? WeekStartDate { get; set; }
        public DateTime? WeekEndDate { get; set; }
        public decimal BudgetAmount { get; set; }
        [NotMapped]
        public string? IsActive { get; set; }

        [NotMapped]
        public bool IsError { get; set; } = false;
        [NotMapped]
        public string? ErrorMessage { get; set; }

    }
}
