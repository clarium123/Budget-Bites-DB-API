using BudgetBitesAPI.Core.Models;
using BudgetBitesAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBitesAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        public readonly IBudgetService _budgetService;
        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpPost]
        public async Task<IActionResult> FetchUserBudget(BudgetModel budgetDetails)
        {
            var budgetList = await _budgetService.UserBudget(budgetDetails);
            return Ok(budgetList);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserBudget(BudgetModel budgetDetails)
        {
            var budgetList = await _budgetService.SaveUserBudget(budgetDetails);
            return Ok(budgetList);
        }
    }
}
