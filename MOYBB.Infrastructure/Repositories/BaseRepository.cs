using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MOYBB.Core.Interfaces;
using MOYBB.Infrastructure.Data;

namespace MOYBB.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _dbSet
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public virtual async Task<int> GetTotalCountAsync()
        {
            return await _dbSet.CountAsync();
        }
    }
} 