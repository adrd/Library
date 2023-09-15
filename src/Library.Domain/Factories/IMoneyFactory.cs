namespace Library.Domain.Factories;

public interface IMoneyFactory
{
    IMoneyFactory WithAmount(decimal amount);
}