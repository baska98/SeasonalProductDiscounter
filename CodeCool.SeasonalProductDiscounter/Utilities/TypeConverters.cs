using CodeCool.SeasonalProductDiscounter.Model.Enums;

namespace CodeCool.SeasonalProductDiscounter.Utilities;

public static class TypeConverters
{
    public static Color GetColorEnum(string s) => Enum.Parse<Color>(s);
    public static Season GetSeasonEnum(string s) => Enum.Parse<Season>(s);

    public static int ToInt(object obj) => Convert.ToInt32(obj);
    public static double ToDouble(object obj) => Convert.ToDouble(obj);
    public static string ToString(object obj) => obj.ToString() ?? "";
    public static DateTime ToDateTime(string s) => DateTime.Parse(s);
}
