﻿using CodeCool.SeasonalProductDiscounter.Model.Offers;
using CodeCool.SeasonalProductDiscounter.Model.Products;
using CodeCool.SeasonalProductDiscounter.Model.Transactions;
using CodeCool.SeasonalProductDiscounter.Model.Users;
using CodeCool.SeasonalProductDiscounter.Service.Discounts;
using CodeCool.SeasonalProductDiscounter.Service.Logger;
using CodeCool.SeasonalProductDiscounter.Service.Products.Repository;
using CodeCool.SeasonalProductDiscounter.Service.Transactions.Repository;
using CodeCool.SeasonalProductDiscounter.Service.Users;

namespace CodeCool.SeasonalProductDiscounter.Service.Transactions.Simulator;

public class TransactionsSimulator
{
    private static readonly Random Random = new();

    private readonly ILogger _logger;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IDiscounterService _discounterService;
    private readonly ITransactionRepository _transactionRepository;

    public TransactionsSimulator(
        ILogger logger,
        IUserRepository userRepository,
        IProductRepository productRepository,
        IAuthenticationService authenticationService,
        IDiscounterService discounterService,
        ITransactionRepository transactionRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
        _productRepository = productRepository;
        _authenticationService = authenticationService;
        _discounterService = discounterService;
        _transactionRepository = transactionRepository;
    }

    public void Run(TransactionsSimulatorSettings settings)
    {

        int successfulTransactions = 0;
        int rounds = 0;

        _logger.LogInfo("Starting simulation");
        while (successfulTransactions <= settings.TransactionsCount && _productRepository.AvailableProducts.Count() != 0) 
        {
            _logger.LogInfo(
                $"Simulation round #{rounds++}, successful transactions: {successfulTransactions}/{settings.TransactionsCount}");

            // Get a random user
            var user = GetRandomUser(settings.UsersCount);
            _logger.LogInfo($"User [{user.UserName}] looking to buy a product");

            // Auth user
            if (!AuthUser(user))
            {
                // If auth is not successful, register the user
                RegisterUser(user);
            }

            // Get user from the repo to have an ID (ID is auto-generated by the database)
            user = GetUserFromRepo(user.UserName);

            // User selects product
            var product = SelectProduct(user);

            // Out of products to sell - terminate cycle
            if (product == null)
            {
                break;
            }

            // Get offer
            var offer = GetOffer(product, settings.Date);

            // Create transaction
            var transaction = CreateTransaction(settings.Date, user, product, offer.Price);

            // Save transaction & set product_sold to TRUE
            if (SaveTransaction(transaction))
            {
                SetProductAsSold(product);
                successfulTransactions++;
            }
            else
            {
                Console.WriteLine("Transaction not added.");
            }
        }
    }


    private static User GetRandomUser(int usersCount)
    {
        return new User(0, $"user{Random.Next(0, usersCount)}", "pw");
    }

    private User GetUserFromRepo(string username)
    {
        Console.WriteLine($"{username}");
        return _userRepository.Get(username);
    }

    private bool AuthUser(User user)
    {
        if (_authenticationService.Authenticate(user))
        {
            return true;
        }
        return false;
    }

    private bool RegisterUser(User user)
    {
        return _userRepository.Add(user);
    }

    private Product? SelectProduct(User user)
    {
        return GetRandomProduct();
    }

    private Product? GetRandomProduct()
    {
        var allProducts = _productRepository.AvailableProducts.ToList();

        if (!allProducts.Any())
        {
            Console.WriteLine("Brak dostępnych produktów w bazie danych.");
            return null;
        }

        var randomProduct = allProducts[Random.Next(0, allProducts.Count)];
        Console.WriteLine($"Wybrany losowy produkt: {randomProduct.Name}");
        return randomProduct;
    }

    private Offer GetOffer(Product product, DateTime date)
    {
        return _discounterService.GetOffer(product, date);
    }

    private static Transaction CreateTransaction(DateTime date, User user, Product product, double price)
    {
        var id = 0;
        return new Transaction(id, date, user, product, price);
    }

    private bool SaveTransaction(Transaction transaction)
    {
        return _transactionRepository.Add(transaction);
    }

    private bool SetProductAsSold(Product product)
    {
        return _productRepository.SetProductAsSold(product);
    }
}