using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetBitesAPI.Core.Models
{
    public class UserModel
    {
        [Key]
        public int PersonID { get; set; } = 0;
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? EmailId { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int FamilyMember { get; set; } = 1;

        [NotMapped]
        public string? FoodPrefered { get; set; }

        [NotMapped]
        public decimal BudgetAmount { get; set; } = 0;

        [NotMapped]
        public string? PreferedCusine { get; set; }

        [NotMapped]
        public bool IsError { get; set; }
        [NotMapped]
        public string? ErrorMessage { get; set; }
        [NotMapped]
        public string? JwtToken { get; set; }
    }
}
