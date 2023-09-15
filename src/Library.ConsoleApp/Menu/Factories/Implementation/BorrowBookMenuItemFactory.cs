using Library.ConsoleApp.Commands.Implementation;
using Library.ConsoleApp.Model;
using Library.ConsoleApp.ValidationRules;
using Library.ConsoleApp.ValidationRules.Implementation.Processor;
using Library.ConsoleApp.ValidationRules.Implementation.Rules;
using Library.Domain.Contracts;
using Library.Domain.Factories.Implementation;

namespace Library.ConsoleApp.Menu.Factories.Implementation;

internal sealed class BorrowBookMenuItemFactory : IMenuItemFactory
{
    private readonly IBorrowBook _borrowBook;

    public BorrowBookMenuItemFactory(IBorrowBook borrowBook)
    {
        this._borrowBook = borrowBook;
    }

    public MenuItem Create()
    {
        return new MenuItem("4. Imprumutare carte", '4', false, true,
                new BorrowBookCommand(new BorrowBookModelRuleProcessor(new List<IInputModelRule<BorrowBookInputModel>>()
                {
                    new BookTitleRequiredRule(),
                    new BookAuthorRequiredRule(),
                    new IsbnRequiredRule(),
                    new IsbnFormatRule(),
                    new ReaderRequiredRule(),
                    new CardRequiredRule(),
                    new CardFormatRule()
                }),
                    new BookFactory(),
                    new ReaderFactory(),
                    this._borrowBook));
    }
}