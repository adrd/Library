namespace Library.Domain.Contracts;

public interface IListOfBooks
{
    IEnumerable<Book> Books { get; }
}