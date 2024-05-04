using BudgetBitesAPI.Core.Models;

namespace BudgetBitesAPI.Services.Interfaces
{
    public interface IBudgetService
    {
        Task<IEnumerable<BudgetModel>> UserBudget(BudgetModel budgetDetails);
        Task<IEnumerable<BudgetModel>> SaveUserBudget(BudgetModel budgetDetails);
    }
}
