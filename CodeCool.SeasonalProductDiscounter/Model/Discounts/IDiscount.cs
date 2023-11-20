using CodeCool.SeasonalProductDiscounter.Model.Products;

namespace CodeCool.SeasonalProductDiscounter.Model.Discounts;

public interface IDiscount
{
    bool Accepts(Product product, DateTime date);

    string Name { get; }

    int Rate { get; }
}
