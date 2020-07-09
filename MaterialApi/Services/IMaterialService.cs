using MaterialApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialApi.Services
{
    public interface IMaterialService
    {
        IEnumerable<Material> Get();

        IEnumerable<Material> GetByPartialName(string name);

        Material GetById(string id);

        Material Add(Material material);

        bool Save(Material material);

        bool Delete(string id);
    }
}
