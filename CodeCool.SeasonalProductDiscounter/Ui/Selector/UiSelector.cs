using CodeCool.SeasonalProductDiscounter.Ui.Factory;

namespace CodeCool.SeasonalProductDiscounter.Ui.Selector;

public class UiSelector
{
    private readonly SortedList<int, UiFactoryBase> _factories;

    public UiSelector(SortedList<int, UiFactoryBase> factories)
    {
        _factories = factories;
    }

    public UiBase Select()
    {
        Console.WriteLine("Welcome to Seasonal Product Discounter v3");

        DisplayMenu();

        int input = 0;
        while (!_factories.ContainsKey(input))
        {
            Console.WriteLine("Please select a screen to show from the list.");
            input = GetIntInput();
        }

        return _factories[input].Create();
    }

    private void DisplayMenu()
    {
        Console.WriteLine("Available screens:");
        foreach (var creator in _factories)
        {
            Console.WriteLine($"{creator.Key} - {creator.Value.UiName}");
        }
    }

    private static int GetIntInput()
    {
        int input = 0;

        while (input == 0)
        {
            var i = Console.ReadLine();
            if (!int.TryParse(i, out input))
            {
                Console.Write("Please provide a valid number!");
            }
        }

        return input;
    }
}
