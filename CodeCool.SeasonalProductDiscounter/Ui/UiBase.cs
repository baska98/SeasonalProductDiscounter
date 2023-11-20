namespace CodeCool.SeasonalProductDiscounter.Ui;

public abstract class UiBase
{
    private readonly string _title;
    public bool RequireAuthentication { get; }

    protected UiBase(string title, bool requireAuthentication)
    {
        RequireAuthentication = requireAuthentication;
        _title = title;
    }

    public void DisplayTitle()
    {
        Console.WriteLine(_title);
    }

    public abstract void Run();
}
