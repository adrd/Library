using Library.Domain.Factories.Implementation;

namespace Library.Domain.Factories;

public interface IReaderFactory
{
    IReaderFactory WithName(Action<ReaderNameFactory> action);

    IReaderFactory WithCard(Action<CardFactory> cardFactory);

    Reader Build();
}