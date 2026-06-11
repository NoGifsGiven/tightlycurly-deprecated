using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace TightlyCurly.Com.Common.Data.DataAccess;

/// <summary>
/// Executes the legacy stored procedures over ADO.NET (Microsoft.Data.SqlClient) and
/// returns data readers, matching the result shapes the Sql* data providers expect.
/// </summary>
public sealed class ObjectDataAccess : IDisposable
{
    private readonly string _connectionString;

    public ObjectDataAccess(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("Connection string cannot be null or empty", nameof(connectionString));
        }
        _connectionString = connectionString;
    }

    internal Task<DbDataReader> GetQuestionByIdAsync(int questionId, CancellationToken cancellationToken = default)
    {
        return ExecuteReaderAsync(DatabaseConstants.StoredProcedures.GetQuestionById, cancellationToken,
            ("questionId", questionId));
    }

    internal Task<DbDataReader> GetQuestionByQuestionAndAnswerAsync(string question, string answer, CancellationToken cancellationToken = default)
    {
        return ExecuteReaderAsync(DatabaseConstants.StoredProcedures.GetQuestionByQuestionAndAnswer, cancellationToken,
            ("question", question),
            ("answer", answer));
    }

    internal Task<DbDataReader> GetQuestionsByCategoryAsync(int questionCategoryId, CancellationToken cancellationToken = default)
    {
        return ExecuteReaderAsync(DatabaseConstants.StoredProcedures.GetQuestionsByCategory, cancellationToken,
            ("questionCategoryId", questionCategoryId));
    }

    internal Task<DbDataReader> GetAllQuestionsAsync(CancellationToken cancellationToken = default)
    {
        return ExecuteReaderAsync(DatabaseConstants.StoredProcedures.GetAllQuestions, cancellationToken);
    }

    internal Task<DbDataReader> GetQuestionsByCriteriaAsync(string criteria, CancellationToken cancellationToken = default)
    {
        return ExecuteReaderAsync(DatabaseConstants.StoredProcedures.GetQuestionsByCriteria, cancellationToken,
            ("criteria", criteria));
    }

    internal Task<DbDataReader> SaveQuestionAsync(int questionId, string question, string answer, int localeId, string questionCategoryIds, CancellationToken cancellationToken = default)
    {
        return ExecuteReaderAsync(DatabaseConstants.StoredProcedures.UpdateQuestion, cancellationToken,
            ("questionId", questionId),
            ("question", question),
            ("answer", answer),
            ("localeId", localeId),
            ("questionCategoryIds", questionCategoryIds));
    }

    internal Task DeleteQuestionByIdAsync(int questionId, CancellationToken cancellationToken = default)
    {
        return ExecuteNonQueryAsync(DatabaseConstants.StoredProcedures.DeleteQuestionById, cancellationToken,
            ("questionId", questionId));
    }

    internal Task DeleteQuestionCategoryByIdAsync(int questionCategoryId, CancellationToken cancellationToken = default)
    {
        return ExecuteNonQueryAsync(DatabaseConstants.StoredProcedures.DeleteQuestionCategoryById, cancellationToken,
            ("questionCategoryId", questionCategoryId));
    }

    internal Task<DbDataReader> GetQuestionCategoriesAsync(int? parentCategoryId, CancellationToken cancellationToken = default)
    {
        return ExecuteReaderAsync(DatabaseConstants.StoredProcedures.GetQuestionCategories, cancellationToken,
            ("parentCategoryId", parentCategoryId.HasValue ? parentCategoryId.Value : (object)DBNull.Value));
    }

    internal Task<DbDataReader> GetQuestionCategoriesByCategoryIdsAsync(string questionCategoryIds, CancellationToken cancellationToken = default)
    {
        return ExecuteReaderAsync(DatabaseConstants.StoredProcedures.GetQuestionCategoriesByCategoryIds, cancellationToken,
            ("questionCategoryIds", questionCategoryIds));
    }

    internal Task<DbDataReader> SaveQuestionCategoryAsync(int questionCategoryId, int? parentCategoryId, string categoryName, CancellationToken cancellationToken = default)
    {
        return ExecuteReaderAsync(DatabaseConstants.StoredProcedures.UpdateQuestionCategory, cancellationToken,
            ("questionCategoryId", questionCategoryId),
            ("parentCategoryId", parentCategoryId.HasValue ? parentCategoryId.Value : (object)DBNull.Value),
            ("category", categoryName));
    }

    internal Task<DbDataReader> GetSettingByIdAsync(int settingId, CancellationToken cancellationToken = default)
    {
        return ExecuteReaderAsync(DatabaseConstants.StoredProcedures.GetSettingById, cancellationToken,
            ("settingId", settingId));
    }

    internal Task<DbDataReader> GetSettingByNameAsync(string settingName, CancellationToken cancellationToken = default)
    {
        return ExecuteReaderAsync(DatabaseConstants.StoredProcedures.GetSettingByName, cancellationToken,
            ("name", settingName));
    }

    internal Task<DbDataReader> GetAllSettingsAsync(CancellationToken cancellationToken = default)
    {
        return ExecuteReaderAsync(DatabaseConstants.StoredProcedures.GetAllSettings, cancellationToken);
    }

    internal Task<DbDataReader> SaveSettingAsync(int settingId, string key, string value, bool isActive, CancellationToken cancellationToken = default)
    {
        return ExecuteReaderAsync(DatabaseConstants.StoredProcedures.UpdateSetting, cancellationToken,
            ("settingId", settingId),
            ("key", key),
            ("value", value),
            ("isActive", isActive));
    }

    internal Task DeleteSettingByIdAsync(int settingId, CancellationToken cancellationToken = default)
    {
        return ExecuteNonQueryAsync(DatabaseConstants.StoredProcedures.DeleteSettingById, cancellationToken,
            ("settingId", settingId));
    }

    internal Task DeleteIngredientByIdAsync(int ingredientId, CancellationToken cancellationToken = default)
    {
        return ExecuteNonQueryAsync(DatabaseConstants.StoredProcedures.DeleteIngredientById, cancellationToken,
            ("ingredientId", ingredientId));
    }

    internal Task DeleteLocaleByIdAsync(int localeId, CancellationToken cancellationToken = default)
    {
        return ExecuteNonQueryAsync(DatabaseConstants.StoredProcedures.DeleteLocaleById, cancellationToken,
            ("localeId", localeId));
    }

    private async Task<DbDataReader> ExecuteReaderAsync(string storedProcedure, CancellationToken cancellationToken, params (string Name, object Value)[] parameters)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        try
        {
            await connection.OpenAsync(cancellationToken);
            await using SqlCommand command = BuildCommand(connection, storedProcedure, parameters);
            // CloseConnection ties the connection lifetime to the returned reader.
            return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection, cancellationToken);
        }
        catch
        {
            await connection.DisposeAsync();
            throw;
        }
    }

    private async Task<int> ExecuteNonQueryAsync(string storedProcedure, CancellationToken cancellationToken, params (string Name, object Value)[] parameters)
    {
        await using SqlConnection connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await using SqlCommand command = BuildCommand(connection, storedProcedure, parameters);
        return await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static SqlCommand BuildCommand(SqlConnection connection, string storedProcedure, (string Name, object Value)[] parameters)
    {
        SqlCommand command = new SqlCommand(storedProcedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        foreach ((string name, object value) in parameters)
        {
            command.Parameters.AddWithValue($"@{name}", value ?? DBNull.Value);
        }
        return command;
    }

    public void Dispose()
    {
        // Connections are scoped to each call; nothing is held open between calls.
    }
}
