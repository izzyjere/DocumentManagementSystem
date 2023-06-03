﻿namespace RTSADocs.Data.Services
{
    public interface IDocumentService : ICrudService<Document>
    {
        IEnumerable<Document> Search(string query);     
    }
}