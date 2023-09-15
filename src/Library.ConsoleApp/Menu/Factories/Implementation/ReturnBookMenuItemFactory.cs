using Library.ConsoleApp.Commands.Implementation;
using Library.ConsoleApp.Model;
using Library.ConsoleApp.ValidationRules;
using Library.ConsoleApp.ValidationRules.Implementation.Processor;
using Library.ConsoleApp.ValidationRules.Implementation.Rules;
using Library.Domain.Contracts;
using Library.Domain.Factories.Implementation;

namespace Library.ConsoleApp.Menu.Factories.Implementation;

internal sealed class ReturnBookMenuItemFactory : IMenuItemFactory
{
    private readonly IReturnBook _returnBook;

    public ReturnBookMenuItemFactory(IReturnBook returnBook)
    {
        this._returnBook = returnBook;
    }

    public MenuItem Create()
    {
        return new MenuItem("5. Restituire carte", '5', false, true,
                new ReturnBookCommand(new ReturnBookModelRuleProcessor(new List<IInputModelRule<ReturnBookInputModel>>()
                {
                    new BookTitleRequiredRule(),
                    new BookAuthorRequiredRule(),
                    new IsbnRequiredRule(),
                    new IsbnFormatRule(),
                    new ReaderRequiredRule(),
                    new CardRequiredRule(),
                    new CardFormatRule(),
                    new ReturnDateRequiredRule(),
                    new ReturnDateFormatRule(),
                    new ReturnDateValidRule()

                }),
                    new BookFactory(),
                    new ReaderFactory(),
                    new DateFactory(),
                    this._returnBook));
    }
}