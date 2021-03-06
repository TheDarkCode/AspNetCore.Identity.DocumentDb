﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Identity.DocumentDb.Stores
{
    public abstract class StoreBase
    {
        protected bool disposed = false;

        protected DocumentClient documentClient;
        protected DocumentDbOptions options;
        protected ILookupNormalizer normalizer;
        protected Uri collectionUri;
        protected string collectionName;

        protected StoreBase(DocumentClient documentClient, IOptions<DocumentDbOptions> options, ILookupNormalizer normalizer, string collectionName)
        {
            this.documentClient = documentClient;
            this.options = options.Value;
            this.normalizer = normalizer;
            this.collectionName = collectionName;

            this.collectionUri = UriFactory.CreateDocumentCollectionUri(this.options.Database, collectionName);
        }

        protected virtual void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        protected Uri GenerateDocumentUri(string documentId)
        {
            return UriFactory.CreateDocumentUri(options.Database, collectionName, documentId);
        }
    }
}
