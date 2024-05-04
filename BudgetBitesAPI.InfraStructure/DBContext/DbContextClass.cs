using BudgetBitesAPI.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetBitesAPI.InfraStructure.DBContext
{
    public class DbContextClass : DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> contextOptions) : base(contextOptions)
        {

        }

        public DbSet<LoginModel> LoginDetails { get; set; }
        public DbSet<UserModel> Persons { get; set; }
        public DbSet<PreferenceModel> PreferenceMaster { get; set; }
        public DbSet<FavouriteModel> Favourites { get; set; }
        public DbSet<BudgetModel> Budget { get; set; }
        public DbSet<MealPlanModel> MealPlan { get; set; }
    }
}
