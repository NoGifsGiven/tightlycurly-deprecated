using System;
using System.Collections.Generic;
using System.Data;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Data.Builders;

public class PageBuilder : IPageBuilder
{
    public IEnumerable<Page> BuildPages(Func<IDataReader> readerDelegate)
    {
        var list = new List<Page>();
        
        using (IDataReader dataReader = readerDelegate())
        {
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    list.Add(LoadPageFromReader(dataReader));
                }
            }
        }
        
        return list;
    }

    public IEnumerable<PageContent> BuildPageContents(Func<IDataReader> readerDelegate)
    {
        var list = new List<PageContent>();
        
        using (IDataReader dataReader = readerDelegate())
        {
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    list.Add(LoadContentFromReader(dataReader));
                }
            }
        }
        
        return list;
    }

    public Page LoadPageFromReader(IDataReader reader)
    {
        var page = new Page();
        
        if (reader != null)
        {
            page.PageId = Convert.ToInt32(reader["PageId"]);
            page.Name = reader["Name"]?.ToString() ?? string.Empty;
            
            // Safe enum parsing with fallback
            var viewStatusValue = reader["ViewStatus"]?.ToString();
            if (!string.IsNullOrEmpty(viewStatusValue) && Enum.TryParse<ViewStatus>(viewStatusValue, true, out var viewStatus))
            {
                page.ViewStatus = viewStatus;
            }
            
            // Safe date parsing with fallback to current time
            var enteredDateValue = reader["EnteredDate"]?.ToString();
            if (!string.IsNullOrEmpty(enteredDateValue) && DateTimeOffset.TryParse(enteredDateValue, out var enteredDate))
            {
                page.EnteredDate = enteredDate;
            }
            
            var updatedDateValue = reader["UpdatedDate"]?.ToString();
            if (!string.IsNullOrEmpty(updatedDateValue) && DateTimeOffset.TryParse(updatedDateValue, out var updatedDate))
            {
                page.UpdatedDate = updatedDate;
            }
        }
        
        return page;
    }

    public PageContent LoadContentFromReader(IDataReader reader)
    {
        var pageContent = new PageContent();
        
        if (reader != null)
        {
            pageContent.PageContentId = Convert.ToInt32(reader["PageContentId"]);
            pageContent.Title = reader["Title"]?.ToString() ?? string.Empty;
            pageContent.Description = reader["Description"]?.ToString() ?? string.Empty;
            pageContent.MetaDescription = reader["MetaDescription"]?.ToString() ?? string.Empty;
            pageContent.MetaKeywords = reader["MetaKeywords"]?.ToString() ?? string.Empty;
            pageContent.Content = reader["Content"]?.ToString() ?? string.Empty;
            
            // Safe boolean parsing with fallback to false
            var isActiveValue = reader["IsActive"];
            if (isActiveValue != DBNull.Value)
            {
                bool.TryParse(isActiveValue.ToString(), out var isActive);
                pageContent.IsActive = isActive;
            }
            else
            {
                pageContent.IsActive = false;
            }
            
            // Safe locale object creation with fallbacks
            if (reader["LocaleId"] != DBNull.Value && reader["LCID"] != DBNull.Value)
            {
                pageContent.Locale = new Locale
                {
                    LocaleId = Convert.ToInt32(reader["LocaleId"]),
                    LCID = Convert.ToInt32(reader["LCID"]),
                    LocaleName = reader["LocaleName"]?.ToString() ?? string.Empty
                };
            }
            
            // Safe date parsing with fallback to current time
            var enteredDateValue = reader["EnteredDate"]?.ToString();
            if (!string.IsNullOrEmpty(enteredDateValue) && DateTimeOffset.TryParse(enteredDateValue, out var enteredDate))
            {
                pageContent.EnteredDate = enteredDate;
            }
            
            var updatedDateValue = reader["UpdatedDate"]?.ToString();
            if (!string.IsNullOrEmpty(updatedDateValue) && DateTimeOffset.TryParse(updatedDateValue, out var updatedDate))
            {
                pageContent.UpdatedDate = updatedDate;
            }
        }
        
        return pageContent;
    }

    // Async methods for .NET 10 support
    public async Task<IEnumerable<Page>> BuildPagesAsync(Func<Task<IDataReader>> readerDelegate, CancellationToken cancellationToken = default)
    {
        var list = new List<Page>();
        
        using (IDataReader dataReader = await readerDelegate())
        {
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    list.Add(await LoadPageFromReaderAsync(dataReader, cancellationToken));
                }
            }
        }
        
        return list;
    }

    public async Task<IEnumerable<PageContent>> BuildPageContentsAsync(Func<Task<IDataReader>> readerDelegate, CancellationToken cancellationToken = default)
    {
        var list = new List<PageContent>();
        
        using (IDataReader dataReader = await readerDelegate())
        {
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    list.Add(await LoadContentFromReaderAsync(dataReader, cancellationToken));
                }
            }
        }
        
        return list;
    }

    public Task<Page> LoadPageFromReaderAsync(IDataReader reader, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(LoadPageFromReader(reader));
    }

    public Task<PageContent> LoadContentFromReaderAsync(IDataReader reader, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(LoadContentFromReader(reader));
    }
}
