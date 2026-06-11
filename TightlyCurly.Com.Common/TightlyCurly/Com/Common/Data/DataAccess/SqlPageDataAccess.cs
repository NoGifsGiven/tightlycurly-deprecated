using System.Data;
using TightlyCurly.Com.Common.Data.Mappers;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Common.Model;
using TightlyCurly.Com.Framework.Extensions;

namespace TightlyCurly.Com.Common.Data.DataAccess;

public class SqlPageDataAccess : SqlDataAccessBase, IPageDataAccess
{
    public SqlPageDataAccess(IConfigurationHelper configurationHelper, IParameterMapper parameterMapper)
        : base(configurationHelper, parameterMapper)
    {
    }

    public IDataReader SavePage(Page page)
    {
        NamedParameter[] namedParameters = new NamedParameter[3]
        {
            new NamedParameter
            {
                Name = "pageId",
                Value = page.PageId
            },
            new NamedParameter
            {
                Name = "name",
                Value = page.Name
            },
            new NamedParameter
            {
                Name = "viewStatus",
                Value = page.ViewStatus.ToInt()
            }
        };
        return ExecuteDataReader("UpdatePage", namedParameters);
    }

    public IDataReader SavePageContent(PageContent pageContent, Page page)
    {
        NamedParameter[] namedParameters = new NamedParameter[9]
        {
            new NamedParameter
            {
                Name = "pageContentId",
                Value = pageContent.PageContentId
            },
            new NamedParameter
            {
                Name = "pageId",
                Value = page.PageId
            },
            new NamedParameter
            {
                Name = "title",
                Value = pageContent.Title
            },
            new NamedParameter
            {
                Name = "metaDescription",
                Value = pageContent.MetaDescription
            },
            new NamedParameter
            {
                Name = "metaKeywords",
                Value = pageContent.MetaKeywords
            },
            new NamedParameter
            {
                Name = "description",
                Value = pageContent.Description
            },
            new NamedParameter
            {
                Name = "content",
                Value = pageContent.Content
            },
            new NamedParameter
            {
                Name = "isActive",
                Value = pageContent.IsActive
            },
            new NamedParameter
            {
                Name = "localeId",
                Value = 1
            }
        };
        return ExecuteDataReader("UpdatePageContent", namedParameters);
    }

    public IDataReader GetPageById(int pageId)
    {
        NamedParameter[] namedParameters = new NamedParameter[1]
        {
            new NamedParameter
            {
                Name = "pageId",
                Value = pageId
            }
        };
        return ExecuteDataReader("GetPageById", namedParameters);
    }

    public IDataReader GetPageByName(string name)
    {
        NamedParameter[] namedParameters = new NamedParameter[1]
        {
            new NamedParameter
            {
                Name = "name",
                Value = name
            }
        };
        return ExecuteDataReader("GetPageByName", namedParameters);
    }

    public IDataReader GetPageContentByPageId(int pageId)
    {
        NamedParameter[] namedParameters = new NamedParameter[1]
        {
            new NamedParameter
            {
                Name = "pageId",
                Value = pageId
            }
        };
        return ExecuteDataReader("GetPageContentByPageId", namedParameters);
    }

    public IDataReader GetPageContentById(int pageContentId)
    {
        NamedParameter[] namedParameters = new NamedParameter[1]
        {
            new NamedParameter
            {
                Name = "pageContentId",
                Value = pageContentId
            }
        };
        return ExecuteDataReader("GetPageContentById", namedParameters);
    }

    public IDataReader GetAllPages()
    {
        return ExecuteDataReader("GetAllPages");
    }

    public void DeletePageById(int pageId)
    {
        NamedParameter[] namedParameters = new NamedParameter[1]
        {
            new NamedParameter
            {
                Name = "pageId",
                Value = pageId
            }
        };
        ExecuteNonQuery("DeletePageById", namedParameters);
    }

    public void DeletePageContentById(int pageContentId)
    {
        NamedParameter[] namedParameters = new NamedParameter[1]
        {
            new NamedParameter
            {
                Name = "pageContentId",
                Value = pageContentId
            }
        };
        ExecuteNonQuery("DeletePageContentById", namedParameters);
    }

    public void SetPageContentActive(int pageId, int pageContentId)
    {
        NamedParameter[] namedParameters = new NamedParameter[2]
        {
            new NamedParameter
            {
                Name = "pageId",
                Value = pageId
            },
            new NamedParameter
            {
                Name = "pageContentId",
                Value = pageContentId
            }
        };
        ExecuteNonQuery("SetPageContentActive", namedParameters);
    }
}
