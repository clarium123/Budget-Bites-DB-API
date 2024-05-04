using BudgetBitesAPI.Core.Models;

namespace BudgetBitesAPI.Services.Interfaces
{
    public interface IUserDetailsService
    {
        Task<UserModel> CreateUser(UserModel userDetails);
        Task<IEnumerable<UserModel>> GetAllLoginUsers();
    }
}
