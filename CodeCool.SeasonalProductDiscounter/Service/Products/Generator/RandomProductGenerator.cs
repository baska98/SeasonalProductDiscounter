using CodeCool.SeasonalProductDiscounter.Extensions;
using CodeCool.SeasonalProductDiscounter.Model.Enums;
using CodeCool.SeasonalProductDiscounter.Model.Products;

namespace CodeCool.SeasonalProductDiscounter.Service.Products.Generator;

public class RandomProductGenerator
{
    private static readonly Random Random = new();
    private static readonly Color[] Colors = Enum.GetValues<Color>();
    private static readonly Season[] Seasons = Enum.GetValues<Season>();

    private static readonly string[] Names =
    {
        "skirt",
        "T-shirt",
        "jacket",
        "shirt",
        "hat",
        "gloves"
    };

    public IEnumerable<Product> Products { get; }

    public RandomProductGenerator(uint count, double minimumPrice, double maximumPrice, bool sold = false)
    {
        Products = GenerateRandomProducts(count, minimumPrice, maximumPrice, sold).ToList();
    }

    private static IEnumerable<Product> GenerateRandomProducts(uint count, double minimumPrice, double maximumPrice, bool sold)
    {
        for (uint i = 0; i < count; i++)
        {
            var color = GetRandomColor();
            yield return new Product(i, GetRandomName(color), color, GetRandomSeason(),
                GetRandomPrice(minimumPrice, maximumPrice), sold);
        }
    }

    private static Color GetRandomColor()
    {
        return Colors[Random.Next(0, Colors.Length)];
    }

    private static string GetRandomName(Color color)
    {
        return $"{color} {Names[Random.Next(0, Names.Length)]}";
    }

    private static Season GetRandomSeason()
    {
        return Seasons[Random.Next(0, Seasons.Length)];
    }

    private static double GetRandomPrice(double minimumPrice, double maximumPrice)
    {
        return Math.Round(Random.NextDouble(minimumPrice, maximumPrice), 1);
    }
}
