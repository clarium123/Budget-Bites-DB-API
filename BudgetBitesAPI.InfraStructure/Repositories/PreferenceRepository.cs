using BudgetBitesAPI.Core.Interfaces;
using BudgetBitesAPI.Core.Models;
using BudgetBitesAPI.InfraStructure.DBContext;

namespace BudgetBitesAPI.InfraStructure.Repositories
{
    public class PreferenceRepository : GenericRepository<PreferenceModel>, IPreferenceRepository
    {
        public PreferenceRepository(DbContextClass context) : base(context)
        {
        }
    }
}
