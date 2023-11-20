using CodeCool.SeasonalProductDiscounter.Model.Enums;
using CodeCool.SeasonalProductDiscounter.Model.Products;

namespace CodeCool.SeasonalProductDiscounter.Service.Products.Statistics;

public interface IProductStatistics
{
    int TotalProducts();
    Product? GetMostExpensive();
    Product? GetCheapest();
    double GetAveragePrice();

    Dictionary<string, double> GetAveragePricesByName();
    Dictionary<Color, double> GetAveragePricesByColor();
    Dictionary<Season, double> GetAveragePricesBySeason();
    Dictionary<PriceRange, double> GetAveragePricesByPriceRange();

    Dictionary<string, int> GetCountByName();
    Dictionary<Color, int> GetCountByColor();
    Dictionary<Season, int> GetCountBySeason();
    Dictionary<PriceRange, int> GetCountByPriceRange();
}
