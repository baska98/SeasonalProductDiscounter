using CodeCool.SeasonalProductDiscounter.Model.Discounts;

namespace CodeCool.SeasonalProductDiscounter.Service.Discounts.Repository;

public interface IDiscountRepository
{
    IEnumerable<IDiscount> Discounts { get; }
}
