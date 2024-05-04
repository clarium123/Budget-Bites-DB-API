using BudgetBitesAPI.Core.Interfaces;
using BudgetBitesAPI.Core.Models;
using BudgetBitesAPI.Security.CryptoJS;
using BudgetBitesAPI.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BudgetBitesAPI.Services.Services
{
    public class LoginService : ILoginService
    {
        public IUnitOfWork _unitOfWork;
        public readonly IConfiguration _config;
        public readonly string key = "b14ca5898a4e4133bbce2ea2315a1916";
        public LoginService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<LoginModel> CreateUser(LoginModel loginDetails)
        {
            var resultData = new LoginModel();
            if (loginDetails != null)
            {
                DataCryption _dataCryption = new DataCryption();
                loginDetails.Password = _dataCryption.EncryptString(key, loginDetails.Password ?? "");
                SqlParameter[] paramList = new SqlParameter[4];
                paramList[0] = new SqlParameter("P_Username", loginDetails.Username);
                paramList[1] = new SqlParameter("P_Password", loginDetails.Password);
                var loginResultData = await _unitOfWork.Login.SqlExecuteUSPRaw("EXEC [dbo].[USP_UserLoginCreation] @P_Username, @P_Password, @P_Role, @P_VendorName", paramList);
                resultData = loginResultData.FirstOrDefault(x => x.Username == loginDetails.Username && x.Password == loginDetails.Password);
                if (resultData != null)
                {
                    resultData.JwtToken = null;
                }
                else
                {
                    resultData = new LoginModel();
                    resultData.IsError = true;
                    resultData.ErrorMessage = loginResultData.ToList()[0].Username;
                }
            }
            return resultData;
        }
        public async Task<IEnumerable<LoginModel>> GetAllLoginUsers()
        {
            var loginDetailsList = await _unitOfWork.Login.GetAll();
            return loginDetailsList;
        }

        public async Task<LoginModel> IsValidUser(LoginModel loginDetails)
        {
            var resultData = new LoginModel();
            if (loginDetails != null)
            {
                DataCryption _dataCryption = new DataCryption();
                loginDetails.Password = _dataCryption.EncryptString(key, loginDetails.Password ?? "");
                SqlParameter[] paramList = new SqlParameter[2];
                paramList[0] = new SqlParameter("P_Username", loginDetails.Username);
                paramList[1] = new SqlParameter("P_Password", loginDetails.Password);

                var loginResultData = await _unitOfWork.Login.SqlExecuteUSPRaw("EXEC [dbo].[USP_UserLoginValidation] @P_Username, @P_Password", paramList);
                resultData = loginResultData.FirstOrDefault(x => x.Username == loginDetails.Username && x.Password == loginDetails.Password);
                if (resultData != null)
                {
                    //TokenGeneration tokenGeneration = new TokenGeneration(_config);
                    //var token = tokenGeneration.GenerateJWTToken(resultData);
                    resultData.JwtToken = null;
                }
                else
                {
                    resultData = new LoginModel();
                    resultData.IsError = true;
                    resultData.ErrorMessage = "Invalid Username or Password";
                }
            }
            return resultData;
        }
    }
}
