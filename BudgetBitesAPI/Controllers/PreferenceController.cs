using BudgetBitesAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBitesAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PreferenceController : ControllerBase
    {
        public readonly IPreferenceService _preferenceService;

        public PreferenceController(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;
        }

        [HttpGet]
        public async Task<IActionResult> FoodPreferenceList()
        {
            var prefernceDataList = await _preferenceService.GetAllFoodPreference();
            if (prefernceDataList == null)
            {
                return NotFound();
            }
            return Ok(prefernceDataList);
        }
    }
}
