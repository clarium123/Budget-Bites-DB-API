using BudgetBitesAPI.Core.Interfaces;
using BudgetBitesAPI.Core.Models;
using BudgetBitesAPI.InfraStructure.DBContext;

namespace BudgetBitesAPI.InfraStructure.Repositories
{
    public class FavouriteRepository : GenericRepository<FavouriteModel>, IFavouriteRepository
    {
        public FavouriteRepository(DbContextClass context) : base(context)
        {
        }
    }
}
