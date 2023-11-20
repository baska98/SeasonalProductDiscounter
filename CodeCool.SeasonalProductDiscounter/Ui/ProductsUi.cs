using CodeCool.SeasonalProductDiscounter.Model.Products;
using CodeCool.SeasonalProductDiscounter.Service.Products.Browser;

namespace CodeCool.SeasonalProductDiscounter.Ui;

public class ProductsUi : UiBase
{
    private readonly IProductBrowser _productBrowser;

    public ProductsUi(IProductBrowser productBrowser, string title, bool requireAuthentication) : base(title, requireAuthentication)
    {
        _productBrowser = productBrowser;
    }

    public override void Run()
    {
        PrintProducts("All products:", _productBrowser.GetAll());
    }

    private static void PrintProducts(string text, IEnumerable<Product> products)
    {
        Console.WriteLine($"{text}: ");
        foreach (var product in products)
        {
            Console.WriteLine(product);
        }
    }
}
