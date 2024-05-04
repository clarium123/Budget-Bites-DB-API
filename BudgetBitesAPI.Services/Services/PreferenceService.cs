using BudgetBitesAPI.Core.Interfaces;
using BudgetBitesAPI.Core.Models;
using BudgetBitesAPI.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BudgetBitesAPI.Services.Services
{
    public class PreferenceService : IPreferenceService
    {
        public IUnitOfWork _unitOfWork;
        public readonly IConfiguration _config;
        public PreferenceService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<IEnumerable<PreferenceModel>> GetAllFoodPreference()
        {
            var loginDetailsList = await _unitOfWork.FoodPreference.GetAll();
            return loginDetailsList;
        }
    }
}
