using Library.Domain;
using Library.Domain.Contracts;

namespace Library.ConsoleApp.Commands.Implementation;

internal sealed class ListOfBooksCommand : ICommand
{
    private readonly IListOfBooks _library;

    public ListOfBooksCommand(IListOfBooks library)
    {
        this._library = library;
    }

    public void Execute()
    {
        IEnumerable<Book> books = this._library.Books;

        this.DisplayBooks(books);
    }

    private void DisplayBooks(IEnumerable<Book> books)
    {
        Console.WriteLine("Lista carti:\n");
        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
    }
}