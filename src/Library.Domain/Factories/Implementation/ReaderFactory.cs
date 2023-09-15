namespace Library.Domain.Factories.Implementation;

public sealed class ReaderFactory : IReaderFactory
{
    private ReaderName _readerName;
    private Card _card;

    public ReaderFactory()
    {
        
    }

    public IReaderFactory WithName(Action<ReaderNameFactory> action)
    {
        ReaderNameFactory readerNameFactory = new ReaderNameFactory();

        action(readerNameFactory);

        ReaderName readerName = readerNameFactory.Build();

        this._readerName = readerName;

        return this;
    }

    public IReaderFactory WithCard(Action<CardFactory> action)
    {
        CardFactory cardFactory = new CardFactory();

        action(cardFactory);

        Card card = cardFactory.Build();

        this._card = card;

        return this;
    }

    public Reader Build()
        => new Reader(this._readerName, this._card);
}