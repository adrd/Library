namespace Library.Domain.Factories;

public interface IDateFactory
{
    IDateFactory With(DateTime date);

    Date Build();
}