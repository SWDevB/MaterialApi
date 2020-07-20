using MaterialApi.Configuration;
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

        #region synchronous

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
            using (IDocumentSession session = _documentStore.OpenSession())
            {
                return session.Query<Material>()
                    .Where(material => material.Id == id)
                    .FirstOrDefault();
            }
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

        #endregion synchronous

        #region asynchronous

        public async Task<Material> AddAsync(Material material)
        {
            using (IAsyncDocumentSession session = _documentStore.OpenAsyncSession())
            {
                //this way RavenDB creates a GUID as ID
                material.Id = string.Empty;
                await session.StoreAsync(material);
                await session.SaveChangesAsync();
                return material;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            bool result = false;
            using (IAsyncDocumentSession session = _documentStore.OpenAsyncSession())
            {
                bool exists = await session.Advanced.ExistsAsync(id);
                if (exists)
                {
                    session.Delete(id);
                    await session.SaveChangesAsync();
                    result = true;
                }
            }

            return result;
        }

        public async Task<IEnumerable<Material>> GetAsync(string nameStartsWith)
        {
            List<Material> result;

            using (IAsyncDocumentSession session = _documentStore.OpenAsyncSession())
            {
                if (!string.IsNullOrEmpty(nameStartsWith))
                    result = await session.Query<Material>()
                        .Where(material => material.Name.StartsWith(nameStartsWith, StringComparison.OrdinalIgnoreCase))
                        .OrderBy(material => material.Name)
                        .ToListAsync();
                else
                    result = await session.Query<Material>()
                        .OrderBy(material => material.Name)
                        .ToListAsync();
            }

            return result;
        }

        public async Task<Material> GetByIdAsync(string id)
        {
            using (IAsyncDocumentSession session = _documentStore.OpenAsyncSession())
            {
                return await session.Query<Material>()
                    .Where(material => material.Id == id)
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<bool> UpdateAsync(Material material)
        {
            if (string.IsNullOrEmpty(material?.Id))
                throw new ArgumentException("Material Id not set");

            bool result = false;
            using (IAsyncDocumentSession session = _documentStore.OpenAsyncSession())
            {
                bool exists = await session.Advanced.ExistsAsync(material.Id);
                if (exists)
                {
                    await session.StoreAsync(material);
                    await session.SaveChangesAsync();
                    result = true;
                }
            }

            return result;
        }

        #endregion
    }
}
