using CodeCool.SeasonalProductDiscounter.Model.Products;
using CodeCool.SeasonalProductDiscounter.Model.Users;

namespace CodeCool.SeasonalProductDiscounter.Model.Transactions;

public record Transaction(int Id, DateTime Date, User User, Product Product, double PricePaid);
