using BudgetBitesAPI.Core.Models;

namespace BudgetBitesAPI.Services.Interfaces
{
    public interface IMealPlanService
    {
        Task<IEnumerable<MealPlanModel>> UserMealPlanDetails(MealPlanModel mealPlanDetails);
        Task<IEnumerable<MealPlanModel>> SaveUserMealPlan(MealDishModel mealPlanDetails);
        Task<IEnumerable<MealPlanModel>> DeleteUserMealPlan(DeleteDishModel mealPlanDetails);
    }
}
