using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetBitesAPI.Core.Models
{
    public class CusineModel
    {
        [Key]
        public int PreferredCusineID { get; set; }
        public int PersonID { get; set; } = 0;
        public string? Username { get; set; }
        public string? PreferredCusine { get; set; }

        [NotMapped]
        public string? IsActive { get; set; }

        [NotMapped]
        public bool IsError { get; set; } = false;
        [NotMapped]
        public string? ErrorMessage { get; set; }
    }
}
