using CodeCool.SeasonalProductDiscounter.Model.Enums;

namespace CodeCool.SeasonalProductDiscounter.Extensions;

public static class SeasonExtensions
{
    private static readonly Dictionary<Season, Month[]> SeasonsToMonths = new()
    {
        { Season.Spring, new[] { Month.March, Month.April, Month.May } },
        { Season.Summer, new[] { Month.June, Month.July, Month.August } },
        { Season.Autumn, new[] { Month.September, Month.October, Month.November } },
        { Season.Winter, new[] { Month.December, Month.January, Month.February } }
    };

    private static readonly Season[] Seasons = SeasonsToMonths.Keys.ToArray();

    public static Season Shift(this Season season, int shift)
    {
        int index = (int)season;
        int shifted = (index + shift) % Seasons.Length;
        return (Season)shifted;
    }

    public static bool Contains(this Season season, DateTime date)
    {
        return SeasonsToMonths[season].Contains((Month)date.Month);
    }
}
