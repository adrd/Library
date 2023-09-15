using System.Globalization;

namespace Library.Domain;

public sealed record Money
{
    public static readonly Money Zero = new Money(decimal.Zero);
    private readonly Decimal _amount;
    private readonly string _currency = "RON";

    public Money(Decimal amount)
    {
        this._amount = amount;
    }

    public static Money operator *(Money money, Int32 factor)
    {
        Decimal result = money._amount * factor;

        return new Money(result);
    }

    public static implicit operator Decimal(Money money)
    {
        return money._amount;
    }

    public static implicit operator Money(Decimal amount)
    {
        return new Money(amount);
    }

    public override string ToString()
    {
        return this._amount.ToString("F2", CultureInfo.InvariantCulture) + " " + this._currency;
    }
}