using CodeCool.SeasonalProductDiscounter.Model.Discounts;
using CodeCool.SeasonalProductDiscounter.Model.Enums;

namespace CodeCool.SeasonalProductDiscounter.Service.Discounts.Repository;

public class DiscountRepository : IDiscountRepository
{
    public IEnumerable<IDiscount> Discounts { get; }

    public DiscountRepository()
    {
        Discounts = GetDiscounts();
    }

    private static IEnumerable<IDiscount> GetDiscounts()
    {
        return new List<IDiscount>
        {
            new MonthlyDiscount("Summer Kick-off", 10, new HashSet<Month> { Month.June, Month.July }),
            new MonthlyDiscount("Winter Sale", 10, new HashSet<Month> { Month.February }),
            new ColorDiscount("Blue Winter", 5, Color.Blue, Season.Winter),
            new ColorDiscount("Green Spring", 5, Color.Green, Season.Spring),
            new ColorDiscount("Yellow Summer", 5, Color.Yellow, Season.Summer),
            new ColorDiscount("Brown Autumn", 5, Color.Brown, Season.Autumn),
            new SeasonalDiscount("Sale Discount", 10, 21),
            new SeasonalDiscount("Outlet Discount", 20, 2)
        };
    }
}
