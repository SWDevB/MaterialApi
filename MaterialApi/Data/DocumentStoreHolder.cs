using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MaterialApi.Data
{
    /// <summary>
    /// This class provides the RavendDB DocumentStore to access the Database
    /// </summary>
    public class DocumentStoreHolder : IDocumentStoreHolder
    {
        public DocumentStoreHolder(IOptions<DocumentStoreSettings> documentStoreSettings)
        {
            var settings = documentStoreSettings.Value;
            X509Certificate2 clientCertificate = null;

            if (!string.IsNullOrEmpty(settings.PathToCertificate))
                clientCertificate = new X509Certificate2(settings.PathToCertificate);

            Store = new DocumentStore
            {
                Urls = new[] { settings.Url },
                Database = settings.Database,
                Certificate = clientCertificate

            }.Initialize();
        }

        public IDocumentStore Store { get; }
    }
}
