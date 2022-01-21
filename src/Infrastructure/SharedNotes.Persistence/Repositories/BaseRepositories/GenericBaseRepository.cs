using Microsoft.EntityFrameworkCore;
using SharedNote.Application.Interfaces.Common;
using SharedNote.Domain.Common;
using SharedNotes.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNotes.Persistence.Repositories.BaseRepositories
{
    public class GenericBaseRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly ApplicationDbContext _context;
        public GenericBaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {

            await _context.AddAsync<TEntity>(entity);
            return entity;

        }

        public Task DeleteAsync(TEntity entity)
        {

            _context.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();         
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.FindAsync<TEntity>(id);
        }
        public TEntity UpdateAsync(TEntity entity)
        {

            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

    }
}
