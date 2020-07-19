using MaterialApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialApi.Services
{
    /// <summary>
    /// Service interface for accessing Materials
    /// </summary>
    public interface IMaterialService
    {
        IEnumerable<Material> Get(string nameStartsWith);

        Material GetById(string id);

        Material Add(Material material);

        bool Update(Material material);

        bool Delete(string id);

        Task<IEnumerable<Material>> GetAsync(string nameStartsWith);

        Task<Material> GetByIdAsync(string id);

        Task<Material> AddAsync(Material material);

        Task<bool> UpdateAsync(Material material);

        Task<bool> DeleteAsync(string id);
    }
}
