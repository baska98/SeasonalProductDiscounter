using CodeCool.SeasonalProductDiscounter.Model.Enums;
using CodeCool.SeasonalProductDiscounter.Model.Products;

namespace CodeCool.SeasonalProductDiscounter.Service.Products.Browser;

public interface IProductBrowser
{
    IEnumerable<Product> GetAll();
    IEnumerable<Product> GetByName(string name);
    IEnumerable<Product> GetByColor(Color color);
    IEnumerable<Product> GetBySeason(Season season);

    IEnumerable<Product> GetByPriceSmallerThan(double price);
    IEnumerable<Product> GetByPriceGreaterThan(double price);
    IEnumerable<Product> GetByPriceRange(double minimumPrice, double maximumPrice);

    IEnumerable<IGrouping<string, Product>> GroupByName();
    IEnumerable<IGrouping<Color, Product>> GroupByColor();
    IEnumerable<IGrouping<Season, Product>> GroupBySeason();
    IEnumerable<IGrouping<PriceRange, Product>> GroupByPriceRange();

    IEnumerable<Product> OrderByName();
    IEnumerable<Product> OrderByColor();
    IEnumerable<Product> OrderBySeason();
    IEnumerable<Product> OrderByPrice();
}
