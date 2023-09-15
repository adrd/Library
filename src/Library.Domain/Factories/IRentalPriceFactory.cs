using Library.Domain.Factories.Implementation;

namespace Library.Domain.Factories;

public interface IRentalPriceFactory
{
    IRentalPriceFactory WithPrice(Action<MoneyFactory> action);
}