
using CodeCool.SeasonalProductDiscounter.Model.Transactions;

namespace CodeCool.SeasonalProductDiscounter.Service.Transactions.Repository;

public interface ITransactionRepository
{
    bool Add(Transaction transaction);
    IEnumerable<Transaction> GetAll();
}
