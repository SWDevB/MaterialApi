using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialApi.Data
{
    /// <summary>
    /// Settings for RavenDB connection
    /// </summary>
    public class DocumentStoreSettings
    {
        public string Url { get; set; }
        public string Database { get; set; }
        public string PathToCertificate { get; set; }
    }
}
