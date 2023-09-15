namespace Library.Domain.Factories.Implementation;

public class MoneyFactory : IMoneyFactory
{
    private Decimal _amount;

    public MoneyFactory()
    {
        this._amount = 0m;
    }

    public IMoneyFactory WithAmount(decimal amount)
    {
        this._amount = amount;
        return this;
    }

    public Money Build()
        => new Money(this._amount);
}