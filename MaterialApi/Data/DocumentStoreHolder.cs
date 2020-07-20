using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MaterialApi.Configuration
{
    /// <summary>
    /// This class provides the RavendDB DocumentStore to access the Database
    /// </summary>
    public class DocumentStoreHolder : IDocumentStoreHolder
    {
        public DocumentStoreHolder(IOptions<DocumentStoreSettings> documentStoreSettings, ILogger<DocumentStoreHolder> logger)
        {
            try
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
            catch (Exception ex)
            {
                logger.LogError(ex, "Error Occured during DocumentStoreHolder creation!");
            }
        }

        public IDocumentStore Store { get; }
    }
}
