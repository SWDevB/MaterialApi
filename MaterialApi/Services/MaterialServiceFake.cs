using MaterialApi.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialApi.Services
{
    public class MaterialServiceFake : IMaterialService
    {
        List<Material> _repository = new List<Material>();

        public MaterialServiceFake()
        {
            _repository.Add(new Material { Author = "Admin", Hidden = false, Id = Guid.NewGuid().ToString(), Name = "Water", Notes = "Most common fluid", Phase = KindOfPhase.Continuous });
            _repository.Add(new Material { Author = "Admin", Hidden = false, Id = Guid.NewGuid().ToString(), Name = "Mercury", Notes = "More poisonous fluid", Phase = KindOfPhase.Continuous });
        }

        public Material Add(Material material)
        {
            material.Id = Guid.NewGuid().ToString();
            _repository.Add(material);
            return material;
        }

        public bool Delete(string id)
        {
            bool result = false;
            Material materialToDelete = _repository.FirstOrDefault(material => material.Id == id);
            if (materialToDelete != null)    
                _repository.Remove(materialToDelete);

            return result;
        }

        public IEnumerable<Material> Get()
        {
            return _repository;
        }

        public Material GetById(string id)
        {
            return _repository.FirstOrDefault(material => material.Id == id);
        }

        public IEnumerable<Material> GetByPartialName(string name)
        {
            return _repository.Where(material => material.Name.Contains(name));
        }

        public bool Save(Material material)
        {
            bool result = false;
            int indexToReplace = _repository.FindIndex(materialToReplace => materialToReplace.Id == material.Id);
            if (indexToReplace > -1)
                _repository[indexToReplace] = material;
  
            return result;
        }
    }
}
