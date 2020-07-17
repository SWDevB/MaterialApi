using MaterialApi.Data;
using MaterialApi.Models;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialApi.Services
{
    /// <summary>
    /// Service to access Materials
    /// </summary>
    public class MaterialService : IMaterialService
    {
        IDocumentStore _documentStore;
        public MaterialService(IDocumentStoreHolder storeHolder)
        {
            _documentStore = storeHolder.Store;
        }

        public Material Add(Material material)
        {
            using (IDocumentSession session = _documentStore.OpenSession())
            {
                //this way RavenDB creates a GUID as ID
                material.Id = string.Empty;
                session.Store(material);
                session.SaveChanges();
                return material;
            }
        }

        public bool Delete(string id)
        {
            bool result = false;
            using (IDocumentSession session = _documentStore.OpenSession())
            {
                bool exists = session.Advanced.Exists(id);
                if (exists)
                {
                    session.Delete(id);
                    session.SaveChanges();
                    result = true;
                }
            }

            return result;
        }

        public IEnumerable<Material> Get(string nameStartsWith)
        {
            List<Material> result;

            using (IDocumentSession session = _documentStore.OpenSession())
            {
                if (!string.IsNullOrEmpty(nameStartsWith))
                    result = session.Query<Material>()
                        .Where(material => material.Name.StartsWith(nameStartsWith, StringComparison.OrdinalIgnoreCase))
                        .OrderBy(material => material.Name)
                        .ToList();
                else
                    result = session.Query<Material>()
                        .OrderBy(material => material.Name)
                        .ToList();
            }

            return result;
        }

        public Material GetById(string id)
        {
            Material result;

            using (IDocumentSession session = _documentStore.OpenSession())
            {
                result = session.Query<Material>()
                    .Where(material => material.Id == id)
                    .FirstOrDefault();
            }

            return result;
        }

        public bool Update(Material material)
        {
            if (string.IsNullOrEmpty(material?.Id))
                throw new ArgumentException("Material Id not set");

            bool result = false;
            using (IDocumentSession session = _documentStore.OpenSession())
            {
                bool exists = session.Advanced.Exists(material.Id);
                if (exists)
                {
                    session.Store(material);
                    session.SaveChanges();
                    result = true;
                }
            }

            return result;
        }
    }
}
