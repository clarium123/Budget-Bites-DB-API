using System.ComponentModel.DataAnnotations;

namespace BudgetBitesAPI.Core.Models
{
    public class PreferenceModel
    {
        [Key]
        public int PreferenceID { get; set; } = 0;
        public string? Preferences { get; set; }
    }
}
