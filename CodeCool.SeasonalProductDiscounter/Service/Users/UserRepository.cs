using CodeCool.SeasonalProductDiscounter.Model.Users;
using CodeCool.SeasonalProductDiscounter.Service.Logger;
using CodeCool.SeasonalProductDiscounter.Service.Persistence;

namespace CodeCool.SeasonalProductDiscounter.Service.Users;

public class UserRepository : SqLiteConnector, IUserRepository
{
    private readonly string _tableName;
    public UserRepository(string dbFile, ILogger logger) : base(dbFile, logger)
    {
        _tableName = DatabaseManager.UsersTableName;
    }

    public IEnumerable<User> GetAll()
    {
        var query = @$"SELECT user_id, user_name, password FROM {_tableName}";
        var ret = new List<User>();

        try
        {
            using var connection = GetPhysicalDbConnection();
            using var command = GetCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var user = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                ret.Add(user);
            }
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            throw;
        }

        return ret;
    }

    public bool Add(User user)
    {
        var userAdded = true;
        var user_id = HowManyRows() + 1;
        var query = $"INSERT INTO {_tableName} (user_id, user_name, password) VALUES ({user_id}, '{user.UserName}', '{user.Password}') ";
        if (!ExecuteNonQuery(query))
        {
            userAdded = false;
            throw new Exception($"{query} not added");
        }
        return userAdded;
    }

    private int HowManyRows()
    {
        var query = $"SELECT COUNT(user_id) FROM {_tableName}";
        using var connection = GetPhysicalDbConnection();
        using var command = GetCommand(query, connection);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            if (!reader.IsDBNull(0))
            {
                return reader.GetInt32(0);
            }
        }

        throw new InvalidOperationException("Failed to retrieve the count of IDs from the database.");

    }

    public User Get(string name)
    {
        var query = $"SELECT user_id, user_name, password FROM {_tableName} WHERE user_name = '{name}'";

        try
        {
            using var connection = GetPhysicalDbConnection();
            using var command = GetCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                return new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return null;
    }
}
