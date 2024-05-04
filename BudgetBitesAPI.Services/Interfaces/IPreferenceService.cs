using BudgetBitesAPI.Core.Models;

namespace BudgetBitesAPI.Services.Interfaces
{
    public interface IPreferenceService
    {
        Task<IEnumerable<PreferenceModel>> GetAllFoodPreference();
    }
}
