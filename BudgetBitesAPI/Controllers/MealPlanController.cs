﻿using BudgetBitesAPI.Core.Models;
using BudgetBitesAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBitesAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MealPlanController : ControllerBase
    {
        public readonly IMealPlanService _mealPlanService;

        public MealPlanController(IMealPlanService mealPlanService)
        {
            _mealPlanService = mealPlanService;
        }        

        [HttpPost]
        public async Task<IActionResult> SaveUserMealPlan(MealDishModel mealPLanDetails)
        {
            var mealPlanList = await _mealPlanService.SaveUserMealPlan(mealPLanDetails);
            return Ok(mealPlanList);
        }

        [HttpPost]
        public async Task<IActionResult> UserMealPlanData(MealPlanModel mealPLanDetails)
        {
            var mealPlanList = await _mealPlanService.UserMealPlanDetails(mealPLanDetails);
            return Ok(mealPlanList);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserMealPlan(DeleteDishModel mealPLanDetails)
        {
            var mealPlanList = await _mealPlanService.DeleteUserMealPlan(mealPLanDetails);
            return Ok(mealPlanList);
        }
    }
}
