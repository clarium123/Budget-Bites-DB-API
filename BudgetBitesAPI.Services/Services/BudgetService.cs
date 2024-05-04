using BudgetBitesAPI.Core.Interfaces;
using BudgetBitesAPI.Core.Models;
using BudgetBitesAPI.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.SqlTypes;

namespace BudgetBitesAPI.Services.Services
{
    public class BudgetService : IBudgetService
    {
        public IUnitOfWork _unitOfWork;
        public readonly IConfiguration _config;

        public BudgetService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<IEnumerable<BudgetModel>> UserBudget(BudgetModel budgetDetails)
        {
            IEnumerable<BudgetModel> resultData = new List<BudgetModel>();
            if (budgetDetails != null)
            {
                SqlParameter[] paramList = new SqlParameter[3];
                paramList[0] = new SqlParameter("P_Username", budgetDetails.Username);
                paramList[1] = new SqlParameter("P_WeekStartDate", budgetDetails.WeekStartDate ?? null);
                paramList[2] = new SqlParameter("P_WeekEndDate", budgetDetails.WeekEndDate ?? null);
                resultData = await _unitOfWork.UserBudget.SqlExecuteUSPRaw("EXEC [dbo].[USP_FetchUserBudget] @P_Username, @P_WeekStartDate, @P_WeekEndDate", paramList);
            }
            return resultData;
        }

        public async Task<IEnumerable<BudgetModel>> SaveUserBudget(BudgetModel budgetDetails)
        {
            IEnumerable<BudgetModel> resultData = new List<BudgetModel>();
            if (budgetDetails != null)
            {
                SqlParameter[] paramList = new SqlParameter[4];
                paramList[0] = new SqlParameter("P_Username", budgetDetails.Username);
                paramList[1] = new SqlParameter("P_WeekStartDate", budgetDetails.WeekStartDate);
                paramList[2] = new SqlParameter("P_WeekEndDate", budgetDetails.WeekEndDate);
                paramList[3] = new SqlParameter("P_BudgetAmount", budgetDetails.BudgetAmount);
                resultData = await _unitOfWork.UserBudget.SqlExecuteUSPRaw("EXEC [dbo].[USP_InsertUserBudget] @P_Username, @P_WeekStartDate, @P_WeekEndDate, @P_BudgetAmount", paramList);
            }
            return resultData;
        }
    }
}
