using BudgetBitesAPI.Core.Interfaces;
using BudgetBitesAPI.Core.Models;
using BudgetBitesAPI.Security.CryptoJS;
using BudgetBitesAPI.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace BudgetBitesAPI.Services.Services
{
    public class UserDetailService : IUserDetailsService
    {
        public IUnitOfWork _unitOfWork;
        public readonly IConfiguration _config;
        public readonly string key = "b14ca5898a4e4133bbce2ea2315a1916";
        public UserDetailService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<UserModel> CreateUser(UserModel userDetails)
        {
            var resultData = new UserModel();
            if (userDetails != null)
            {
                DataCryption _dataCryption = new DataCryption();
                userDetails.Password = _dataCryption.EncryptString(key, userDetails.Password ?? "");
                SqlParameter[] paramList = new SqlParameter[13];               
                paramList[0] = new SqlParameter("P_Firstname", userDetails.Firstname);
                paramList[1] = new SqlParameter("P_Lastname", userDetails.Lastname);
                paramList[2] = new SqlParameter("P_Username", userDetails.Username);
                paramList[3] = new SqlParameter("P_Password", userDetails.Password);
                paramList[4] = new SqlParameter("P_EmailId", userDetails.EmailId);
                paramList[5] = new SqlParameter("P_Phone", userDetails.Phone);
                paramList[6] = new SqlParameter("P_Address", userDetails.Address);
                paramList[7] = new SqlParameter("P_City", userDetails.City);
                paramList[8] = new SqlParameter("P_State", userDetails.State);
                paramList[9] = new SqlParameter("P_FamilyMember", userDetails.FamilyMember);
                paramList[10] = new SqlParameter("P_FoodPrefered", userDetails.FoodPrefered);
                paramList[11] = new SqlParameter("P_BudgetAmount", userDetails.BudgetAmount);
                paramList[12] = new SqlParameter("P_PreferedCusine", userDetails.PreferedCusine);
                var userResultData = await _unitOfWork.UserDetails.SqlExecuteUSPRaw("EXEC [dbo].[USP_UserRegistration] @P_Firstname, @P_Lastname, @P_Username, @P_Password, @P_EmailId, @P_Phone, @P_Address, @P_City, @P_State, @P_FamilyMember, @P_FoodPrefered, @P_BudgetAmount, @P_PreferedCusine", paramList);
                resultData = userResultData.FirstOrDefault(x => x.Username == userDetails.Username);
                if (resultData != null)
                {
                    resultData.JwtToken = null;
                }
                else
                {
                    resultData = new UserModel();
                    resultData.IsError = true;
                    resultData.ErrorMessage = userResultData.ToList()[0].Username;
                }
            }
            return resultData;
        }

        public async Task<IEnumerable<UserModel>> GetAllLoginUsers()
        {
            var loginDetailsList = await _unitOfWork.UserDetails.GetAll();
            return loginDetailsList;
        }
    }
}
