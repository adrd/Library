using Library.Domain.Factories.Implementation;

namespace Library.Domain.Factories;

public interface IBookFactory
{
    IBookFactory WithName(Action<BookNameFactory> action);

    IBookFactory WithIsbn(Action<IsbnFactory> action);

    IBookFactory WithRentalPrice(Action<RentalPriceFactory> action);

    Book Build();
}