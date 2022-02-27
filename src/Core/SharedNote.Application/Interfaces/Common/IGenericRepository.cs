using SharedNote.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.Interfaces.Common
{
    public interface IGenericRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        Task<TEntity> AddAsync(TEntity entity);
        TEntity UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByIdAsync(string id);
    }
}
