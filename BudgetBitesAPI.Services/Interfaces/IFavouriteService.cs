using BudgetBitesAPI.Core.Models;

namespace BudgetBitesAPI.Services.Interfaces
{
    public interface IFavouriteService
    {
        Task<IEnumerable<FavouriteModel>> UserFavouriteFood(FavouriteModel favouriteDetails);
        Task<IEnumerable<FavouriteModel>> SaveUserFavouriteFood(FavouriteModel favouriteDetails);
    }
}
