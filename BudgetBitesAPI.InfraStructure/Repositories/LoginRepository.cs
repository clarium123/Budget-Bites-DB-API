using BudgetBitesAPI.Core.Interfaces;
using BudgetBitesAPI.Core.Models;
using BudgetBitesAPI.InfraStructure.DBContext;

namespace BudgetBitesAPI.InfraStructure.Repositories
{
    public class LoginRepository : GenericRepository<LoginModel>, ILoginRepository
    {
        public LoginRepository(DbContextClass context) : base(context)
        {
        }
    }
}
