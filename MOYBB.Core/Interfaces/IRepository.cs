using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOYBB.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize);
        Task<int> GetTotalCountAsync();
    }
} 