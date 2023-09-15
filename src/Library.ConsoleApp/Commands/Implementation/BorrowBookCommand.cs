using Library.ConsoleApp.Model;
using Library.ConsoleApp.ValidationRules;
using Library.Domain;
using Library.Domain.Contracts;
using Library.Domain.Factories;

namespace Library.ConsoleApp.Commands.Implementation;

internal sealed class BorrowBookCommand : ICommand
{
    private readonly IInputModelRuleProcessor<BorrowBookInputModel> _ruleProcessor;
    private readonly IBookFactory _bookFactory;
    private readonly IReaderFactory _readerFactory;
    private readonly IBorrowBook _borrowBook;

    public BorrowBookCommand(IInputModelRuleProcessor<BorrowBookInputModel> ruleProcessor, 
        IBookFactory bookFactory, IReaderFactory readerFactory, IBorrowBook borrowBook)
    {
        this._ruleProcessor = ruleProcessor;
        this._bookFactory = bookFactory;
        this._readerFactory = readerFactory;
        this._borrowBook = borrowBook;
    }

    public void Execute()
    {
        string consoleValues = this.ReadValuesFromConsole();

        BorrowBookInputModel inputModel = this.CreateInputModel(consoleValues);

        (bool passedRules, IEnumerable<string> errors) = this._ruleProcessor.PassesAllRules(inputModel);

        if (!passedRules)
        {
            this.DisplayMessage(this.FormatMessages(errors));
        }
        else
        {
            Book book = this.CreateBook(inputModel);
            Reader reader = this.CreateReader(inputModel);

            if (!this._borrowBook.CanBorrow(book, reader))
            {
                this.DisplayMessage("Cartea nu poate fi imprumutata");
            }
            else
            {
                this._borrowBook.Borrow(book, reader, new Date(DateTime.Today));
                this.DisplayMessage("Cartea a fost imprumutata");
            }
        }
    }

    private string ReadValuesFromConsole()
    {
        string consoleValues = string.Empty;
        while (this.ValuesCount(consoleValues) != 5)
        {
            Console.WriteLine("Introduceti Titlu carte, Autor carte, ISBN (format ISBN 13 cifre: 978-606-92679-1-2), Cititor (Ionut Popescu) si " +
                              "Numar card (6 cifre, format: 123456) despartite prin virgula");
            consoleValues = Console.ReadLine();
        }

        return consoleValues;
    }

    private int ValuesCount(string values)
    {
        return values.Split(',').Length;
    }

    private BorrowBookInputModel CreateInputModel(string consoleValues)
    {
        string[] values = consoleValues.Split(',');

        BorrowBookInputModel inputModel = new BorrowBookInputModel();
        inputModel.BookTitle = values[0].Trim();
        inputModel.BookAuthor = values[1].Trim();
        inputModel.Isbn = values[2].Trim();
        inputModel.ReaderName = values[3].Trim();
        inputModel.CardNumber = values[4].Trim();

        return inputModel;
    }

    private Book CreateBook(BorrowBookInputModel borrowBookInputModel)
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

    private Reader CreateReader(BorrowBookInputModel borrowBookInputModel)
    {
        Reader reader = this._readerFactory.WithName(readerName => readerName
                                                  .With(borrowBookInputModel.ReaderName.ToLower()))
                                             .WithCard(card => card
                                                 .WithNumber(int.Parse(borrowBookInputModel.CardNumber)))
                                             .Build();
        return reader;
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