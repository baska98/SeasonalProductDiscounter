namespace CodeCool.SeasonalProductDiscounter.Service.Persistence;

public interface IDatabaseManager
{
    bool CreateTables();
    bool ClearTables();
}
