using CodeCool.SeasonalProductDiscounter.Model.Discounts;
using CodeCool.SeasonalProductDiscounter.Model.Products;

namespace CodeCool.SeasonalProductDiscounter.Model.Offers;

public record Offer(Product Product, DateTime Date, IEnumerable<IDiscount> Discounts, double Price)
{
    public override string ToString()
    {
        return
            $"{nameof(Product)}: {Product}, " +
            $"{nameof(Date)}: {Date}, " +
            $"{nameof(Discounts)}: {string.Join(",", Discounts)}, " +
            $"{nameof(Price)}: {Price}";
    }
}
