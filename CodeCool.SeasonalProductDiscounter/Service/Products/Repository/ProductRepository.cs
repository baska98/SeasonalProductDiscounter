using CodeCool.SeasonalProductDiscounter.Model.Enums;
using CodeCool.SeasonalProductDiscounter.Model.Products;
using CodeCool.SeasonalProductDiscounter.Service.Logger;
using CodeCool.SeasonalProductDiscounter.Service.Persistence;
using System.Globalization;

namespace CodeCool.SeasonalProductDiscounter.Service.Products.Repository;

public class ProductRepository : SqLiteConnector, IProductRepository
{
    private readonly string _tableName;

    public IEnumerable<Product> AvailableProducts => GetAvailableProducts();

    public ProductRepository(string dbFile, ILogger logger) : base(dbFile, logger)
    {
        _tableName = DatabaseManager.ProductsTableName;
    }

    private IEnumerable<Product> GetAvailableProducts()
    {
        var query = $"SELECT product_id, product_name, color, season, product_price, sold FROM {_tableName} WHERE sold = 0";
        var ret = new List<Product>();

        try
        {
            using var connection = GetPhysicalDbConnection();
            using var command = GetCommand(query, connection);
            using var reader = command.ExecuteReader();
            Logger.LogInfo($"{GetType().Name} executing query: {query}");

            while (reader.Read())
            {
                ret.Add(new Product((uint)reader.GetInt32(0), reader.GetString(1), (Color)reader.GetInt32(2), (Season)reader.GetInt32(3), reader.GetDouble(4), reader.GetBoolean(5)));
            }
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            throw;
        }

        return ret;
    }

    public bool Add(IEnumerable<Product> products)
    {
        bool allAdded = true;
        foreach (var product in products)
        {
            var product_id = HowManyRows() + 1;
            var priceString = product.Price.ToString(CultureInfo.InvariantCulture);
            var query = $"INSERT INTO {_tableName} (product_id, product_name, color, season, product_price, sold) " +
                $"VALUES ({product_id}, '{product.Name}', '{product.Color}', '{product.Season}', {priceString}, {product.Sold}) ";
            if (!ExecuteNonQuery(query))
            {
                allAdded = false;
                throw new Exception($"{query} not added");
            }
        }

        return allAdded;
    }

    private int HowManyRows()
    {
        var query = $"SELECT COUNT(product_id) FROM {_tableName}";
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

    public bool SetProductAsSold(Product product)
    {
        string query = $"UPDATE {_tableName} SET sold = 1 WHERE product_id = {product.Id};";
        return ExecuteNonQuery(query);
    }
}
