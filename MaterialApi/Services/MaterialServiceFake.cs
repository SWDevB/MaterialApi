using MaterialApi.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MaterialApi.Services
{
    /// <summary>
    /// Fake Service to access Materials without persisting Data
    /// </summary>
    public class MaterialServiceFake : IMaterialService
    {
        /// <summary>
        /// Repository is public to enable tests to check it without using the tested code
        /// </summary>
        public List<Material> Repository { get; private set; }

        public MaterialServiceFake(bool addInitialData = true)
        {
            Repository = new List<Material>();

            if (addInitialData)
            {
                Repository.Add(new Material { Author = "Admin", Hidden = false, Id = Guid.NewGuid().ToString(), Name = "Water", Notes = "Most common fluid", Phase = KindOfPhase.Continuous });
                Repository.Add(new Material { Author = "Admin", Hidden = false, Id = Guid.NewGuid().ToString(), Name = "Mercury", Notes = "Poisonous fluid", Phase = KindOfPhase.Continuous });
            }
        }

        #region synchronous

        public Material Add(Material material)
        {
            material.Id = Guid.NewGuid().ToString();
            Repository.Add(material);
            return material;
        }

        public bool Delete(string id)
        {
            bool result = false;
            Material materialToDelete = Repository.FirstOrDefault(material => material.Id == id);
            if (materialToDelete != null)
            {
                Repository.Remove(materialToDelete);
                result = true;
            }

            return result;
        }

        public IEnumerable<Material> Get()
        {
            return Repository;
        }

        public Material GetById(string id)
        {
            return Repository.FirstOrDefault(material => material.Id == id);
        }

        public IEnumerable<Material> Get(string nameStartsWith)
        {
            if (!string.IsNullOrEmpty(nameStartsWith))
                return Repository.Where(material => material.Name.StartsWith(nameStartsWith, StringComparison.OrdinalIgnoreCase));
            else
                return Repository;
        }

        public bool Update(Material material)
        {
            if (string.IsNullOrEmpty(material?.Id))
                throw new ArgumentException("Material Id not set");

            bool result = false;
            int indexToReplace = Repository.FindIndex(materialToReplace => materialToReplace.Id == material.Id);
            if (indexToReplace > -1)
            {
                Repository[indexToReplace] = material;
                result = true;
            }

            return result;
        }

        #endregion

        #region asynchronous

        public Task<IEnumerable<Material>> GetAsync(string nameStartsWith)
        {
            return Task.FromResult(Get(nameStartsWith));
        }

        public Task<Material> GetByIdAsync(string id)
        {
            return Task.FromResult(GetById(id));
        }

        public Task<Material> AddAsync(Material material)
        {
            return Task.FromResult(Add(material));
        }

        public Task<bool> UpdateAsync(Material material)
        {
            return Task.FromResult(Update(material));
        }

        public Task<bool> DeleteAsync(string id)
        {
            return Task.FromResult(Delete(id));
        }

        #endregion
    }
}
