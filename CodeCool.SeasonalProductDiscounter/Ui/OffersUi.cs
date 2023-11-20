using CodeCool.SeasonalProductDiscounter.Model.Offers;
using CodeCool.SeasonalProductDiscounter.Service.Offers;

namespace CodeCool.SeasonalProductDiscounter.Ui;

public class OffersUi : UiBase
{
    private readonly IOfferService _offerService;

    public OffersUi(IOfferService offerService, string title, bool requireAuthentication) : base(title,
        requireAuthentication)
    {
        _offerService = offerService;
    }

    public override void Run()
    {
        var offers = _offerService.GetOffers(DateTime.Today);
        PrintOffers($"Offers available on {DateTime.Today}", offers);
    }

    private static void PrintOffers(string text, IEnumerable<Offer> offers)
    {
        Console.WriteLine($"{text}: ");
        foreach (var offer in offers)
        {
            Console.WriteLine(offer);
        }
    }
}
