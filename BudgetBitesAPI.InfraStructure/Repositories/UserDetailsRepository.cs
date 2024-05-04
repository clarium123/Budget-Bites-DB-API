using BudgetBitesAPI.Core.Interfaces;
using BudgetBitesAPI.Core.Models;
using BudgetBitesAPI.InfraStructure.DBContext;

namespace BudgetBitesAPI.InfraStructure.Repositories
{
    public class UserDetailsRepository : GenericRepository<UserModel>, IUserDetailsRepository
    {
        public UserDetailsRepository(DbContextClass context) : base(context)
        {
        }
    }
}
