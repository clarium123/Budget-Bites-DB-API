namespace BudgetBitesAPI.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ILoginRepository Login { get; }
        IUserDetailsRepository UserDetails { get; }
        IPreferenceRepository FoodPreference { get; }
        IFavouriteRepository FavouriteFood { get; }
        IBudgetRepository UserBudget {  get; }
        IMealPlanRepository MealPlan { get; }
        int Save();
    }
}
