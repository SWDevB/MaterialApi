using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace MaterialApi.Data
{
    public interface IDocumentStoreHolder
    {
        IDocumentStore Store { get; }
    }
}
