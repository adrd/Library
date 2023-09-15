using Library.ConsoleApp.Model;
using Library.ConsoleApp.ValidationRules;
using Library.Domain;
using Library.Domain.Contracts;
using Library.Domain.Factories;
using System.Globalization;

namespace Library.ConsoleApp.Commands.Implementation;

internal sealed class AddBookCommand : ICommand
{
    private readonly IInputModelRuleProcessor<AddBookInputModel> _ruleProcessor;
    private readonly IBookFactory _bookFactory;
    private readonly IAddBook _library;

    public AddBookCommand(IInputModelRuleProcessor<AddBookInputModel> ruleProcessor, IBookFactory bookFactory, IAddBook library)
    {
        this._ruleProcessor = ruleProcessor;
        this._bookFactory = bookFactory;
        this._library = library;
    }

    public void Execute()
    {
        string consoleValues = this.ReadValuesFromConsole();

        AddBookInputModel inputModel = this.CreateInputModel(consoleValues);

        (bool passedRules, IEnumerable<string> errors) = this._ruleProcessor.PassesAllRules(inputModel);

        if (!passedRules)
        {
            this.DisplayMessage(this.FormatMessages(errors));
        }
        else
        {
            Book book = this.Create(inputModel);

            this._library.Add(book);

            this.DisplayMessage("Carte adaugata cu succes.");
        }
    }

    private string ReadValuesFromConsole()
    {
        string consoleValues = string.Empty;
        while (this.ValuesCount(consoleValues) != 4)
        {
            Console.WriteLine("Introduceti Titlu carte, Autor carte, ISBN (format ISBN 13 cifre: 978-606-92679-1-2) si pret de inchiriere (format pret: 45.00) despartite prin virgula");
            consoleValues = Console.ReadLine();
        }

        return consoleValues;
    }

    private int ValuesCount(string values)
    {
        return values.Split(',').Length;
    }

    private AddBookInputModel CreateInputModel(string consoleValues)
    {
        string[] values = consoleValues.Split(',');

        AddBookInputModel inputModel = new AddBookInputModel();
        inputModel.BookTitle = values[0].Trim();
        inputModel.BookAuthor = values[1].Trim();
        inputModel.Isbn = values[2].Trim();
        inputModel.RentalPrice = values[3].Trim();

        return inputModel;
    }

    private Book Create(AddBookInputModel addBookInputModel)
    {
        Book book = this._bookFactory.WithName(bookNameFactory => bookNameFactory
                                        .WithTitle(addBookInputModel.BookTitle.ToLower())
                                        .WithAuthor(addBookInputModel.BookAuthor.ToLower()))
                                     .WithIsbn(isbnFactory => isbnFactory
                                         .With(addBookInputModel.Isbn))
                                     .WithRentalPrice(rentalPriceFactory => rentalPriceFactory
                                         .WithPrice(moneyFactory => moneyFactory
                                             .WithAmount(decimal.Parse(addBookInputModel.RentalPrice, CultureInfo.InvariantCulture))
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