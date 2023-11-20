using CodeCool.SeasonalProductDiscounter.Service.Products;
using CodeCool.SeasonalProductDiscounter.Service.Products.Generator;

namespace CodeCool.SeasonalProductDiscounterTest;

public class RandomProductGeneratorTest
{
    private const double MinimumPrice = 8;
    private const double MaximumPrice = 50;

    [Test]
    public void ReturnsZeroProducts()
    {
        var generator = new RandomProductGenerator(0, MinimumPrice, MaximumPrice);

        Assert.That(generator.Products.Count(), Is.EqualTo(0));
    }

    [Test]
    public void ReturnsCorrectNumberOfProducts()
    {
        uint count = 10;
        var generator = new RandomProductGenerator(count, MinimumPrice, MaximumPrice);

        Assert.That(generator.Products.Count(), Is.EqualTo(count));
    }

    [Test]
    public void ProductPropertiesAreSet()
    {
        uint count = 10;
        var generator = new RandomProductGenerator(count, MinimumPrice, MaximumPrice);

        foreach (var product in generator.Products)
        {
            Assert.Multiple(() =>
            {
                Assert.That(product.Name, Is.Not.Null);
                Assert.That(product.Id, Is.GreaterThanOrEqualTo(0));
                Assert.That(product.Price, Is.GreaterThan(0));
            });
        }
    }

    [Test]
    public void ProductIdsAreDifferent()
    {
        uint count = 10;
        var generator = new RandomProductGenerator(count, MinimumPrice, MaximumPrice);

        var ids = generator.Products.Select(p => p.Id);
        Assert.That(ids, Is.Unique);
    }

    [Test]
    public void ProductNameContainsColor()
    {
        uint count = 10;
        var generator = new RandomProductGenerator(count, MinimumPrice, MaximumPrice);

        foreach (var product in generator.Products)
        {
            Assert.That(product.Name, Does.Contain(product.Color.ToString()));
        }
    }

    [Test]
    public void ProductPriceIsWithinRange()
    {
        uint count = 10;
        var generator = new RandomProductGenerator(count, MinimumPrice, MaximumPrice);

        foreach (var product in generator.Products)
        {
            Assert.That(product.Price, Is.GreaterThan(MinimumPrice));
            Assert.That(product.Price, Is.LessThan(MaximumPrice));
        }
    }
}
