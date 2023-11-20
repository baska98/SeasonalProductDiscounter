using CodeCool.SeasonalProductDiscounter.Model.Enums;
using CodeCool.SeasonalProductDiscounter.Model.Products;
using CodeCool.SeasonalProductDiscounter.Service.Products.Browser;

namespace CodeCool.SeasonalProductDiscounter.Service.Products.Statistics;

public class ProductStatistics : IProductStatistics
{
    private readonly IProductBrowser _browser;

    public ProductStatistics(IProductBrowser browser)
    {
        _browser = browser;
    }

    public int TotalProducts()
    {
        return _browser.GetAll().Count();
    }

    public Product? GetMostExpensive()
    {
        return _browser.GetAll().MaxBy(p => p.Price);
    }

    public Product? GetCheapest()
    {
        return _browser.GetAll().MinBy(p => p.Price);
    }

    public double GetAveragePrice()
    {
        return _browser.GetAll().Average(p => p.Price);
    }

    public Dictionary<string, double> GetAveragePricesByName()
    {
        return _browser.GetAll()
            .GroupBy(p => p.Name, p => p)
            .ToDictionary(g => g.Key, g => g.Average(p => p.Price));
    }

    public Dictionary<Color, double> GetAveragePricesByColor()
    {
        return _browser.GetAll()
            .GroupBy(p => p.Color, p => p)
            .ToDictionary(g => g.Key, g => g.Average(p => p.Price));
    }

    public Dictionary<Season, double> GetAveragePricesBySeason()
    {
        return _browser.GetAll()
            .GroupBy(p => p.Season, p => p)
            .ToDictionary(g => g.Key, g => g.Average(p => p.Price));
    }

    public Dictionary<PriceRange, double> GetAveragePricesByPriceRange()
    {
        return _browser.GroupByPriceRange().ToDictionary(g => g.Key, g => g.Average(p => p.Price));
    }


    public Dictionary<string, int> GetCountByName()
    {
        return _browser.GetAll()
            .GroupBy(p => p.Name, p => p)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    public Dictionary<Color, int> GetCountByColor()
    {
        return _browser.GetAll()
            .GroupBy(p => p.Color, p => p)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    public Dictionary<Season, int> GetCountBySeason()
    {
        return _browser.GetAll()
            .GroupBy(p => p.Season, p => p)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    public Dictionary<PriceRange, int> GetCountByPriceRange()
    {
        return _browser.GroupByPriceRange().ToDictionary(g => g.Key, g => g.Count());

    }
}
