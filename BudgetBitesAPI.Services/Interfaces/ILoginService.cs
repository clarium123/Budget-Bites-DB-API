using BudgetBitesAPI.Core.Models;

namespace BudgetBitesAPI.Services.Interfaces
{
    public interface ILoginService
    {
        Task<LoginModel> IsValidUser(LoginModel userId);
    }
}
