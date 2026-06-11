using System;
using System.Collections.Generic;
using System.Data;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Data.Builders;

public interface IPageBuilder
{
    // Synchronous methods (backward compatible)
    IEnumerable<Page> BuildPages(Func<IDataReader> readerDelegate);
    
    IEnumerable<PageContent> BuildPageContents(Func<IDataReader> readerDelegate);
    
    Page LoadPageFromReader(IDataReader reader);
    
    PageContent LoadContentFromReader(IDataReader reader);

    // Async methods (new for .NET 10)
    Task<IEnumerable<Page>> BuildPagesAsync(Func<Task<IDataReader>> readerDelegate, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<PageContent>> BuildPageContentsAsync(Func<Task<IDataReader>> readerDelegate, CancellationToken cancellationToken = default);

    // Async loading methods (new for .NET 10)
    Task<Page> LoadPageFromReaderAsync(IDataReader reader, CancellationToken cancellationToken = default);
    
    Task<PageContent> LoadContentFromReaderAsync(IDataReader reader, CancellationToken cancellationToken = default);
}