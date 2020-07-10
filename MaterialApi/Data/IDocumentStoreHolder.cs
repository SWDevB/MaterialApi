using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace MaterialApi.Data
{
    /// <summary>
    /// Interface for providing the RavendDB DocumentStore to access the Database
    /// </summary>
    public interface IDocumentStoreHolder
    {
        IDocumentStore Store { get; }
    }
}
