using Library.ConsoleApp.Commands.Implementation;
using Library.ConsoleApp.Model;
using Library.ConsoleApp.ValidationRules;
using Library.ConsoleApp.ValidationRules.Implementation.Processor;
using Library.ConsoleApp.ValidationRules.Implementation.Rules;
using Library.Domain.Contracts;
using Library.Domain.Factories.Implementation;

namespace Library.ConsoleApp.Menu.Factories.Implementation;

internal sealed class NumberOfCopiesOfABookMenuItemFactory : IMenuItemFactory
{
    private readonly INumberOfCopies _numberOfCopies;

    public NumberOfCopiesOfABookMenuItemFactory(INumberOfCopies numberOfCopies)
    {
        this._numberOfCopies = numberOfCopies;
    }

    public MenuItem Create()
    {
        return new MenuItem("3. Numar exemplare disponibile pentru o carte", '3', false, true,
                new NumberOfCopiesOfABookCommand(new NumberOfCopiesOfABookRuleProcessor(
                        new List<IInputModelRule<NumberOfCopiesOfABookInputModel>>()
                        {
                            new BookTitleRequiredRule(),
                            new BookAuthorRequiredRule(),
                            new IsbnRequiredRule(),
                            new IsbnFormatRule(),

                        }),
                    new BookFactory(),
                    this._numberOfCopies));
    }
}