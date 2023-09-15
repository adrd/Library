namespace Library.Domain.Factories.Implementation;

public class CardFactory : ICardFactory
{
    private int _number;

    public CardFactory()
    {
        this._number = 0;
    }

    public ICardFactory WithNumber(int number)
    {
        this._number = number;
        return this;
    }

    public Card Build()
        => new Card(this._number);
}