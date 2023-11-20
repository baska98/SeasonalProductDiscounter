using CodeCool.SeasonalProductDiscounter.Model.Products;

namespace CodeCool.SeasonalProductDiscounter.Service.Products.Repository;

public interface IProductRepository
{
    IEnumerable<Product> AvailableProducts { get; }
    bool Add(IEnumerable<Product> products);
    bool SetProductAsSold(Product product);

}
