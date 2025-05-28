using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Specifications;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
            }
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;

        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if(entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Set<T>().AnyAsync(e => e.Id == id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
            }
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);
        }
    }
}
