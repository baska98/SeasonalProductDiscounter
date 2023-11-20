using CodeCool.SeasonalProductDiscounter.Model.Offers;

namespace CodeCool.SeasonalProductDiscounter.Service.Offers;

public interface IOfferService
{
    public IEnumerable<Offer> GetOffers(DateTime date);
}
