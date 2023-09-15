using Library.ConsoleApp.Model;
using Library.ConsoleApp.ValidationRules;
using Library.Domain;
using Library.Domain.Contracts;
using Library.Domain.Factories;
using System.Globalization;

namespace Library.ConsoleApp.Commands.Implementation;

internal sealed class ReturnBookCommand : ICommand
{
    private readonly IInputModelRuleProcessor<ReturnBookInputModel> _ruleProcessor;
    private readonly IBookFactory _bookFactory;
    private readonly IReaderFactory _readerFactory;
    private readonly IDateFactory _dateFactory;
    private readonly IReturnBook _library;

    public ReturnBookCommand(IInputModelRuleProcessor<ReturnBookInputModel> ruleProcessor, IBookFactory bookFactory, 
        IReaderFactory readerFactory, IDateFactory dateFactory, IReturnBook library)
    {
        this._ruleProcessor = ruleProcessor;
        this._bookFactory = bookFactory;
        this._readerFactory = readerFactory;
        this._dateFactory = dateFactory;
        this._library = library;
    }

    public void Execute()
    {
        string consoleValues = this.ReadValuesFromConsole();

        ReturnBookInputModel inputModel = this.CreateInputModel(consoleValues);

        (bool passedRules, IEnumerable<string> errors) = this._ruleProcessor.PassesAllRules(inputModel);

        if (!passedRules)
        {
            this.DisplayMessage(this.FormatMessages(errors));
        }
        else
        {
            Book book = this.CreateBook(inputModel);
            Reader reader = this.CreateReader(inputModel);
            Date returnDate = this.CreateDate(inputModel);

            if (!this._library.CanReturn(book, reader, returnDate))
            {
                this.DisplayMessage("Cartea nu a fost imprumutata de dvs.");
                return;
            }

            if (this._library.CheckReaderPenalty(book, reader, returnDate))
            {
                Money amount = this._library.ComputeReaderPenalty(book, reader, returnDate);
                this.DisplayMessage($"Trebuie platita o penalitate in cuantum de {amount}.");
            }

            this._library.Return(book, reader, returnDate);
            
            this.DisplayMessage("Carte returnata cu succes.");
        }
    }

    private string ReadValuesFromConsole()
    {
        string consoleValues = string.Empty;
        while (this.ValuesCount(consoleValues) != 6)
        {
            Console.WriteLine("Introduceti Titlu carte, Autor carte, ISBN (format ISBN 13 cifre: 978-606-92679-1-2), " +
                              "Cititor (Ionut Popescu), Numar card (6 cifre, format: 123456) si data (format: zz.ll.aaaa) despartite prin virgula");
            consoleValues = Console.ReadLine();
        }

        return consoleValues;
    }

    private int ValuesCount(string values)
    {
        return values.Split(',').Length;
    }

    private ReturnBookInputModel CreateInputModel(string consoleValues)
    {
        string[] values = consoleValues.Split(',');

        ReturnBookInputModel inputModel = new ReturnBookInputModel();
        inputModel.BookTitle = values[0].Trim();
        inputModel.BookAuthor = values[1].Trim();
        inputModel.Isbn = values[2].Trim();
        inputModel.ReaderName = values[3].Trim();
        inputModel.CardNumber = values[4].Trim();
        inputModel.ReturnDate = values[5].Trim();

        return inputModel;
    }

    private Book CreateBook(ReturnBookInputModel borrowBookInputModel)
    {
        Book book = this._bookFactory.WithName(bookNameFactory => bookNameFactory
                                        .WithTitle(borrowBookInputModel.BookTitle.ToLower())
                                        .WithAuthor(borrowBookInputModel.BookAuthor.ToLower()))
                                     .WithIsbn(isbnFactory => isbnFactory
                                        .With(borrowBookInputModel.Isbn))
                                     .WithRentalPrice(rentalPriceFactory => rentalPriceFactory
                                        .WithPrice(moneyFactory => moneyFactory
                                          .WithAmount(0)
                                     ))
                                 .Build();

        return book;
    }

    private Reader CreateReader(ReturnBookInputModel borrowBookInputModel)
    {
        Reader reader = this._readerFactory.WithName(readerName => readerName
                                                .With(borrowBookInputModel.ReaderName.ToLower()))
                                            .WithCard(card => card
                                                .WithNumber(int.Parse(borrowBookInputModel.CardNumber)))
                                            .Build();
        return reader;
    }

    private Date CreateDate(ReturnBookInputModel borrowBookInputModel)
    {
        Date date = this._dateFactory.With(DateTime.ParseExact(borrowBookInputModel.ReturnDate, "d.M.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None))
                                     .Build(); 

        return date;
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