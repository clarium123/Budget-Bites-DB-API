using BudgetBitesAPI.Core.Interfaces;
using BudgetBitesAPI.Core.Models;
using BudgetBitesAPI.InfraStructure.DBContext;

namespace BudgetBitesAPI.InfraStructure.Repositories
{
    public class MealPlanRepository : GenericRepository<MealPlanModel>, IMealPlanRepository
    {
        public MealPlanRepository(DbContextClass context) : base(context)
        {
        }
    }
}
