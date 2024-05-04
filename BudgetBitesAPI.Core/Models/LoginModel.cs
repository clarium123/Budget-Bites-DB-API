using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BudgetBitesAPI.Core.Models
{
    public class LoginModel
    {
        [Key]
        public int PersonID { get; set; } = 0;
        public string? Username { get; set; }
        public string? Password { get; set; }
        [NotMapped]
        public bool IsError { get; set; }
        [NotMapped]
        public string? ErrorMessage { get; set; }
        [NotMapped]
        public string? JwtToken { get; set; }
    }
}
