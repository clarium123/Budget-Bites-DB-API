using Microsoft.Data.SqlClient;

namespace BudgetBitesAPI.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<IEnumerable<T>> SqlRawQuery(string statement);
        Task<IEnumerable<T>> SqlExecuteUSPRaw(string statement, SqlParameter[] parms);
    }
}
