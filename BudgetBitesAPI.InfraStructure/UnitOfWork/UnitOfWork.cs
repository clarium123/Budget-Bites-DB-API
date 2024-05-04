using BudgetBitesAPI.Core.Interfaces;
using BudgetBitesAPI.InfraStructure.DBContext;
using BudgetBitesAPI.InfraStructure.Repositories;

namespace BudgetBitesAPI.InfraStructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextClass _dbContext;
        
        public ILoginRepository Login { get; set; }
        public IUserDetailsRepository UserDetails { get; set; }
        public IPreferenceRepository FoodPreference { get; set; }
        public IFavouriteRepository FavouriteFood { get; set; }
        public IBudgetRepository UserBudget { get; set; }
        public IMealPlanRepository MealPlan { get; set; }

        public UnitOfWork(DbContextClass dbContext)
        {
            _dbContext = dbContext;
            Login = new LoginRepository(_dbContext);
            UserDetails = new UserDetailsRepository(_dbContext);
            FoodPreference = new PreferenceRepository(_dbContext);
            FavouriteFood = new FavouriteRepository(_dbContext);
            UserBudget = new BudgetRepository(_dbContext);
            MealPlan = new MealPlanRepository(_dbContext);
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
