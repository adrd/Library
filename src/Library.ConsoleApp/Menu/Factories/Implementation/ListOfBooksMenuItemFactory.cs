using Library.ConsoleApp.Commands.Implementation;
using Library.Domain.Contracts;

namespace Library.ConsoleApp.Menu.Factories.Implementation;

internal sealed class ListOfBooksMenuItemFactory : IMenuItemFactory
{
    private readonly IListOfBooks _listOfBooks;

    public ListOfBooksMenuItemFactory(IListOfBooks listOfBooks)
    {
        this._listOfBooks = listOfBooks;
    }

    public MenuItem Create()
    {
        return new MenuItem("2. Lista carti biblioteca", '2', false, true,
                new ListOfBooksCommand(this._listOfBooks));
    }
}