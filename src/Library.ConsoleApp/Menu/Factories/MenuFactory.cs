using Library.ConsoleApp.Menu.Factories.Implementation;

namespace Library.ConsoleApp.Menu.Factories;

internal class MenuFactory
{
    private readonly Domain.Library _library;

    public MenuFactory(Domain.Library library)
    {
        this._library = library;
    }

    public IEnumerable<MenuItem> Create()
    {
        List<MenuItem> menu = new List<MenuItem>()
        {
            new AddBookMenuItemFactory(this._library).Create(),
            new ListOfBooksMenuItemFactory(this._library).Create(),
            new NumberOfCopiesOfABookMenuItemFactory(this._library).Create(),
            new BorrowBookMenuItemFactory(this._library).Create(),
            new ReturnBookMenuItemFactory(this._library).Create(),
            new DoNothingMenuItemFactory().Create(),
            new QuitMenuItemFactory().Create()
        };

        return menu;
    }
}