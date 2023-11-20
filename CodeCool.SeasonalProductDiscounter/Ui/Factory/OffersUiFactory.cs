using CodeCool.SeasonalProductDiscounter.Service.Offers;
using CodeCool.SeasonalProductDiscounter.Service.Users;

namespace CodeCool.SeasonalProductDiscounter.Ui.Factory;

public class OffersUiFactory : UiFactoryBase
{
    private readonly IOfferService _offerService;

    public OffersUiFactory(IOfferService offerService)
    {
        _offerService = offerService;
    }

    public override string UiName => "Offers";

    public override UiBase Create()
    {
        return new OffersUi(_offerService, UiName, false);
    }
}
