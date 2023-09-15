namespace Library.Domain.Factories.Implementation;

public class RentalPriceFactory : IRentalPriceFactory
{
    private Money _amount;

    public RentalPriceFactory()
    {
        this._amount = Money.Zero;
    }

    public IRentalPriceFactory WithPrice(Action<MoneyFactory> action)
    {
        MoneyFactory moneyFactory = new MoneyFactory();

        action(moneyFactory);

        Money amount = moneyFactory.Build();    

        this._amount = amount;

        return this;
    }

    public Price Build()
        => new Price(this._amount);
}