namespace Library.Domain;

public sealed record Reader
{
    private readonly ReaderName _name;
    private readonly Card _card;

    public Reader(ReaderName name, Card card)
    {
        this._name = name;
        this._card = card;
    }

    public override string ToString()
    {
        return this._name.ToString();
    }
}