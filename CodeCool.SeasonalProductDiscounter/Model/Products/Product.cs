using CodeCool.SeasonalProductDiscounter.Model.Enums;

namespace CodeCool.SeasonalProductDiscounter.Model.Products;

public record Product(uint Id, string Name, Color Color, Season Season, double Price, bool Sold)
{
    public override string ToString()
    {
        return
            $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Color)}: {Color}, {nameof(Season)}: {Season}, {nameof(Price)}: {Price}";
    }
}
