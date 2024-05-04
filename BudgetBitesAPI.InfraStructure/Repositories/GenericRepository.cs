using BudgetBitesAPI.Core.Interfaces;
using BudgetBitesAPI.InfraStructure.DBContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BudgetBitesAPI.InfraStructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContextClass _dbContext;

        protected GenericRepository(DbContextClass context)
        {
            _dbContext = context;
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public async Task<IEnumerable<T>> SqlRawQuery(string state)
        {
            return await _dbContext.Set<T>().FromSqlRaw(state).ToListAsync();
        }

        public async Task<IEnumerable<T>> SqlExecuteUSPRaw(string statement, SqlParameter[] parms)
        {
            return await _dbContext.Set<T>().FromSqlRaw(statement, parms).ToListAsync();
        }
    }
}
