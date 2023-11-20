using CodeCool.SeasonalProductDiscounter.Service.Logger;
using Microsoft.Data.Sqlite;

namespace CodeCool.SeasonalProductDiscounter.Service.Persistence;

public abstract class SqLiteConnector
{
    private readonly string _dbFile;
    protected readonly ILogger Logger;

    protected SqLiteConnector(string dbFile, ILogger logger)
    {
        _dbFile = dbFile;
        Logger = logger;
    }

    protected SqliteConnection GetPhysicalDbConnection()
    {
        var dbConnection = new SqliteConnection($"Data Source ={_dbFile};Mode=ReadWrite");
        dbConnection.Open();
        return dbConnection;
    }

    protected static SqliteCommand GetCommand(string query, SqliteConnection connection)
    {
        return new SqliteCommand
        {
            CommandText = query,
            Connection = connection,
        };
    }

    protected bool ExecuteNonQuery(string query)
    {
        try
        {
            using var connection = GetPhysicalDbConnection();
            using var command = GetCommand(query, connection);
            Logger.LogInfo($"{GetType().Name} executing query: {query}");
            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            return false;
        }

        return true;
    }

    protected bool ExecuteNonQuery(IEnumerable<string> queries)
    {
        try
        {
            var result = true;
            foreach (var query in queries)
            {
                if (!ExecuteNonQuery(query))
                {
                    result = false; 
                    break;
                }
            }
            return result;
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            return false;
        }
    }
}
