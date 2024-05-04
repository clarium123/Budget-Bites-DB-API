using BudgetBitesAPI.Core.Interfaces;
using BudgetBitesAPI.Core.Models;
using BudgetBitesAPI.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BudgetBitesAPI.Services.Services
{
    public class FavouriteService : IFavouriteService
    {
        public IUnitOfWork _unitOfWork;
        public readonly IConfiguration _config;

        public FavouriteService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<IEnumerable<FavouriteModel>> UserFavouriteFood(FavouriteModel favouriteDetails)
        {
            IEnumerable<FavouriteModel> resultData = new List<FavouriteModel>();
            if (favouriteDetails != null)
            {
                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("P_Username", favouriteDetails.Username);
                resultData = await _unitOfWork.FavouriteFood.SqlExecuteUSPRaw("EXEC [dbo].[USP_UserFavouriteFood] @P_Username", paramList);
            }
            return resultData;
        }

        public async Task<IEnumerable<FavouriteModel>> SaveUserFavouriteFood(FavouriteModel favouriteDetails)
        {
            IEnumerable<FavouriteModel> resultData = new List<FavouriteModel>();
            if (favouriteDetails != null)
            {
                SqlParameter[] paramList = new SqlParameter[3];
                paramList[0] = new SqlParameter("P_Username", favouriteDetails.Username);
                paramList[1] = new SqlParameter("P_FavouriteDish", favouriteDetails.FavouriteDish);
                paramList[2] = new SqlParameter("P_IsActive", favouriteDetails.IsActive);
                resultData = await _unitOfWork.FavouriteFood.SqlExecuteUSPRaw("EXEC [dbo].[USP_InsertUserFavouriteFood] @P_Username, @P_FavouriteDish, @P_IsActive", paramList);
            }
            return resultData;
        }
    }
}
