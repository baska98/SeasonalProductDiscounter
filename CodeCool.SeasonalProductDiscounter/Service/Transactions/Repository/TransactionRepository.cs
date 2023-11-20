using System.Data;
using System.Globalization;
using CodeCool.SeasonalProductDiscounter.Model.Products;
using CodeCool.SeasonalProductDiscounter.Model.Transactions;
using CodeCool.SeasonalProductDiscounter.Model.Users;
using CodeCool.SeasonalProductDiscounter.Service.Logger;
using CodeCool.SeasonalProductDiscounter.Service.Persistence;
using CodeCool.SeasonalProductDiscounter.Utilities;

namespace CodeCool.SeasonalProductDiscounter.Service.Transactions.Repository;

public class TransactionRepository : SqLiteConnector, ITransactionRepository
{
    private readonly string _tableName;

    public TransactionRepository(string dbFile, ILogger logger) : base(dbFile, logger)
    {
        _tableName = DatabaseManager.TransactionsTableName;
    }

    public bool Add(Transaction transaction)
    {
        var transactionAdded = true;
        var id = HowManyRows() + 1;
        var priceString = transaction.PricePaid.ToString(CultureInfo.InvariantCulture);
        var dateFormatted = transaction.Date.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        var query = $"INSERT INTO {_tableName} (id, date, user_id, product_id, price_paid) " +
            $"VALUES ({id}, '{dateFormatted}', {transaction.User.Id}, {transaction.Product.Id}, {priceString}) ";
        if (!ExecuteNonQuery(query))
        {
            transactionAdded = false;
            throw new Exception($"{query} not added");
        }
        return transactionAdded;
    }
    private int HowManyRows()
    {
        var query = $"SELECT COUNT(id) FROM {_tableName} ";
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
    public IEnumerable<Transaction> GetAll()
    {
        var query = $"SELECT * " +
            $"FROM {_tableName} " +
            $"JOIN {DatabaseManager.UsersTableName} ON {_tableName}.user_id = {DatabaseManager.UsersTableName}.user_id " +
            $"JOIN {DatabaseManager.ProductsTableName} ON {_tableName}.product_id = {DatabaseManager.ProductsTableName}.product_id";


        try
        {
            using var connection = GetPhysicalDbConnection();
            using var command = GetCommand(query, connection);
            using var reader = command.ExecuteReader();
            Logger.LogInfo($"{GetType().Name} executing query: {query}");


            var dt = new DataTable();

            //This is required otherwise the DataTable tries to force the DB constrains on the result set, which can cause problems in some cases (e.g. UNIQUE)
            using var ds = new DataSet { EnforceConstraints = false };
            ds.Tables.Add(dt);
            dt.Load(reader);
            ds.Tables.Remove(dt);

            var lst = new List<Transaction>();
            foreach (DataRow row in dt.Rows)
            {
                var user = ToUser(row);
                var product = ToProduct(row);
                var transaction = ToTransaction(row, user, product);

                lst.Add(transaction);
            }

            return lst;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static User ToUser(DataRow row)
    {
        var id = TypeConverters.ToInt(row[0]);
        var userName = TypeConverters.ToString(row[1]);
        var password = TypeConverters.ToString(row[2]); 

        return new User(id, userName, password);
    }

   private static Product ToProduct(DataRow row)
{
    var id = (uint)TypeConverters.ToInt(row[0]);
    var productName = TypeConverters.ToString(row[1]);
    var color = TypeConverters.GetColorEnum(row[2].ToString());
    var season = TypeConverters.GetSeasonEnum(row[3].ToString());
    var price = TypeConverters.ToDouble(row[4]);
    var sold = TypeConverters.ToInt(row[5]) != 0;
    return new Product(id, productName, color, season, price, sold);
}


    private static Transaction ToTransaction(DataRow row, User user, Product product)
    {
        var id = TypeConverters.ToInt(row[0]);
        var date = TypeConverters.ToDateTime(row[1].ToString());
        var price = TypeConverters.ToDouble(row[4]);
        return new Transaction(id, date, user, product, price);
    }
}
