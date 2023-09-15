namespace Library.Domain.Factories;

public interface ICardFactory
{
    ICardFactory WithNumber(int number);
}