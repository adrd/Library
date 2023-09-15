using System.Globalization;
using Library.ConsoleApp.Model;
using Library.ConsoleApp.ValidationRules;
using Library.Domain;
using Library.Domain.Contracts;
using Library.Domain.Factories;

namespace Library.ConsoleApp.Commands.Implementation;

internal sealed class NumberOfCopiesOfABookCommand : ICommand
{
    private readonly IInputModelRuleProcessor<NumberOfCopiesOfABookInputModel> _ruleProcessor;
    private readonly IBookFactory _bookFactory;
    private readonly INumberOfCopies _library;

    public NumberOfCopiesOfABookCommand(IInputModelRuleProcessor<NumberOfCopiesOfABookInputModel> ruleProcessor,IBookFactory bookFactory, INumberOfCopies library)
    {
        this._ruleProcessor = ruleProcessor;
        this._bookFactory = bookFactory;
        this._library = library;
    }

    public void Execute()
    {
        string consoleValues = this.ReadValuesFromConsole();

        NumberOfCopiesOfABookInputModel inputModel = this.CreateInputModel(consoleValues);

        (bool passedRules, IEnumerable<string> errors) = this._ruleProcessor.PassesAllRules(inputModel);

        if (!passedRules)
        {
            this.DisplayMessage(this.FormatMessages(errors));
        }
        else
        {
            Book book = this.Create(inputModel);

            int numberOfCopies = this._library.NumberOfCopies(book);

            Console.WriteLine("Numar exemplare: {0}", numberOfCopies);
        }
    }

    private string ReadValuesFromConsole()
    {
        string consoleValues = string.Empty;
        while (this.ValuesCount(consoleValues) != 3)
        {
            Console.WriteLine("Introduceti Titlu carte, Autor carte, ISBN (format ISBN 13 cifre: 978-606-92679-1-2) despartite prin virgula");
            consoleValues = Console.ReadLine();
        }

        return consoleValues;
    }

    private int ValuesCount(string values)
    {
        return values.Split(',').Length;
    }

    private NumberOfCopiesOfABookInputModel CreateInputModel(string consoleValues)
    {
        string[] values = consoleValues.Split(',');

        NumberOfCopiesOfABookInputModel inputModel = new NumberOfCopiesOfABookInputModel();
        inputModel.BookTitle = values[0].Trim();
        inputModel.BookAuthor = values[1].Trim();
        inputModel.Isbn = values[2].Trim();

        return inputModel;
    }

    private Book Create(NumberOfCopiesOfABookInputModel inputModel)
    {
        Book book = this._bookFactory.WithName(bookNameFactory => bookNameFactory
                                        .WithTitle(inputModel.BookTitle.ToLower())
                                        .WithAuthor(inputModel.BookAuthor.ToLower()))
                                     .WithIsbn(isbnFactory => isbnFactory
                                        .With(inputModel.Isbn))
                                     .WithRentalPrice(rentalPriceFactory => rentalPriceFactory
                                        .WithPrice(moneyFactory => moneyFactory
                                            .WithAmount(Convert.ToDecimal(0, CultureInfo.InvariantCulture))
                                        ))
                                    .Build();

        return book;
    }

    private void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    private string FormatMessages(IEnumerable<string> messages)
    {
        return string.Join('\n', messages);
    }
}