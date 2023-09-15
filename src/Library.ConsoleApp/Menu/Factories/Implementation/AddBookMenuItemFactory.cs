using Library.ConsoleApp.Commands.Implementation;
using Library.ConsoleApp.Model;
using Library.ConsoleApp.ValidationRules;
using Library.ConsoleApp.ValidationRules.Implementation.Processor;
using Library.ConsoleApp.ValidationRules.Implementation.Rules;
using Library.Domain.Contracts;
using Library.Domain.Factories.Implementation;

namespace Library.ConsoleApp.Menu.Factories.Implementation;

internal sealed class AddBookMenuItemFactory : IMenuItemFactory
{
    private readonly IAddBook _addBook;

    public AddBookMenuItemFactory(IAddBook addBook)
    {
        this._addBook = addBook;
    }

    public MenuItem Create()
    {
        return new MenuItem("1. Adaugare carte in biblioteca", '1', false, true,
                new AddBookCommand(new AddBookInputModelRuleProcessor(new List<IInputModelRule<AddBookInputModel>>()
                    {
                        new BookTitleRequiredRule(),
                        new BookAuthorRequiredRule(),
                        new IsbnRequiredRule(),
                        new IsbnFormatRule(),
                        new RentalPriceRequiredRule(),
                        new RentalPriceFormatRule(),
                        new RentalPriceValidRule()
                    }), 
                    new BookFactory(),
                    this._addBook));
    }
}