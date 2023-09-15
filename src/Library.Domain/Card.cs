namespace Library.Domain;

public sealed record Card
{
    private readonly int _number;

    public Card(int number)
    {
        this._number = number;
    }

    public override string ToString()
    {
        return this._number.ToString();
    }
}