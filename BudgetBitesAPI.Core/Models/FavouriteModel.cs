using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetBitesAPI.Core.Models
{
    public class FavouriteModel
    {
        [Key]
        public int FavouriteID { get; set; } = 0;
        public int PersonID { get; set; } = 0;
        public string? Username { get; set; }
        public string? FavouriteDish { get; set; }
        [NotMapped]
        public string? IsActive { get; set; }

        [NotMapped]
        public bool IsError { get; set; } = false;
        [NotMapped]
        public string? ErrorMessage { get; set; }
    }
}
