using CodeCool.SeasonalProductDiscounter.Extensions;
using CodeCool.SeasonalProductDiscounter.Model.Enums;
using CodeCool.SeasonalProductDiscounter.Model.Products;

namespace CodeCool.SeasonalProductDiscounter.Model.Discounts;

public record ColorDiscount(string Name, int Rate, Color Color, Season Season) : IDiscount
{
    public bool Accepts(Product product, DateTime date)
    {
        return Color == product.Color && Season.Contains(date);
    }

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Rate)}: {Rate}, {nameof(Color)}: {Color}, {nameof(Season)}: {Season}";
    }
}
