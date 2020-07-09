using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialApi.Data
{
    public class DocumentStoreHolder : IDocumentStoreHolder
    {
        public DocumentStoreHolder(IOptions<DocumentStoreSettings> documentStoreSettings)
        {
            var settings = documentStoreSettings.Value;

            Store = new DocumentStore
            { 
                Urls = new[] { settings.Url },
                Database = settings.Database
            }.Initialize();
        }

        public IDocumentStore Store { get; }
    }
}
