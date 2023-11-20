using CodeCool.SeasonalProductDiscounter.Model.Offers;
using CodeCool.SeasonalProductDiscounter.Service.Discounts;
using CodeCool.SeasonalProductDiscounter.Service.Products.Browser;

namespace CodeCool.SeasonalProductDiscounter.Service.Offers;

public class OfferService : IOfferService
{
    private readonly IProductBrowser _productBrowser;
    private readonly IDiscounterService _discounterService;

    public OfferService(IProductBrowser productBrowser, IDiscounterService discounterService)
    {
        _productBrowser = productBrowser;
        _discounterService = discounterService;
    }

    public IEnumerable<Offer> GetOffers(DateTime date)
    {
        return _productBrowser.GetAll()
            .Select(p => _discounterService.GetOffer(p, date))
            .Where(o => o.Discounts.Any());
    }
}
