using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace TightlyCurly.Com.Common.Data.Mappers;

public class SqlParameterMapper : IParameterMapper
{
    private static readonly Dictionary<string, DbType> KnownParameters = new();

    static SqlParameterMapper()
    {
        DefineMappings();
    }

    public IEnumerable<DbParameter> GetParameters(IEnumerable<NamedParameter> namedParameters)
    {
        var list = new List<DbParameter>();

        if (namedParameters != null && namedParameters.Any())
        {
            foreach (var namedParameter in namedParameters)
            {
                list.Add(GetParameter(namedParameter));
            }
        }

        return list;
    }

    private SqlParameter GetParameter(NamedParameter namedParameter)
    {
        if (namedParameter == null)
        {
            throw new ArgumentNullException(nameof(namedParameter));
        }

        if (!KnownParameters.TryGetValue(namedParameter.Name, out DbType dbType))
        {
            throw new InvalidOperationException($"Could not locate database parameter for named parameter: {namedParameter.Name}");
        }

        // A new parameter instance is created per call; the original implementation
        // mutated shared instances, which is not thread safe.
        return new SqlParameter
        {
            ParameterName = $"@{namedParameter.Name}",
            Direction = ParameterDirection.Input,
            DbType = dbType,
            Value = namedParameter.Value ?? DBNull.Value
        };
    }

    private static void DefineMappings()
    {
        // Ingredient parameters
        AddMapping("ingredientId", DbType.Int32);
        AddMapping("title", DbType.String);
        AddMapping("alias", DbType.String);
        AddMapping("description", DbType.String);
        AddMapping("links", DbType.String);
        AddMapping("hairRatings", DbType.String);
        AddMapping("hairTypes", DbType.String);
        AddMapping("ingredientCategoryIds", DbType.String);
        AddMapping("isActive", DbType.Boolean);
        AddMapping("notes", DbType.String);
        AddMapping("ingredientStatus", DbType.Int32);
        AddMapping("comments", DbType.String);

        // User parameters
        AddMapping("emailAddress", DbType.String);
        AddMapping("firstName", DbType.String);
        AddMapping("lastName", DbType.String);
        AddMapping("preferredFormat", DbType.Int32);
        AddMapping("isPrivate", DbType.Boolean);

        // Ingredient Category parameters
        AddMapping("ingredientCategoryId", DbType.Int32);
        AddMapping("ingredientCategoryName", DbType.String);
        AddMapping("category", DbType.String);
        AddMapping("name", DbType.String);

        // Ingredient Rating parameters
        AddMapping("ingredientRatingId", DbType.Int32);

        // Question parameters
        AddMapping("questionId", DbType.Int32);
        AddMapping("question", DbType.String);
        AddMapping("answer", DbType.String);
        AddMapping("questionCategoryIds", DbType.String);
        AddMapping("questionCategoryId", DbType.Int32);

        // Question Category parameters
        AddMapping("parentCategoryId", DbType.Int32);
        AddMapping("categoryName", DbType.String);

        // Criteria parameters
        AddMapping("criteria", DbType.String);

        // Message parameters
        AddMapping("messageText", DbType.String);
        AddMapping("messageSubject", DbType.String);

        // Key-Value Store parameters
        AddMapping("key", DbType.String);
        AddMapping("value", DbType.String);

        // Settings parameters
        AddMapping("settingId", DbType.Int32);
        AddMapping("settingName", DbType.String);

        // Campaign parameters
        AddMapping("campaignName", DbType.String);

        // Page parameters
        AddMapping("pageId", DbType.Int32);
        AddMapping("pageContentId", DbType.Int32);
        AddMapping("metaDescription", DbType.String);
        AddMapping("metaKeywords", DbType.String);
        AddMapping("content", DbType.String);
        AddMapping("viewStatus", DbType.Int32);
        AddMapping("localeId", DbType.Int32);

        // Url entry/route parameters
        AddMapping("urlEntryId", DbType.Int32);
        AddMapping("urlRouteId", DbType.Int32);
        AddMapping("routeName", DbType.String);
        AddMapping("routeUrl", DbType.String);
        AddMapping("handlerPath", DbType.String);
        AddMapping("routeType", DbType.Int32);
        AddMapping("urlEntryIds", DbType.String);
        AddMapping("uri", DbType.String);
        AddMapping("priority", DbType.String);
        AddMapping("changeFrequency", DbType.Int32);
        AddMapping("objectType", DbType.Int32);
        AddMapping("objectId", DbType.Int32);
    }

    private static void AddMapping(string name, DbType dbType)
    {
        KnownParameters.Add(name, dbType);
    }
}
