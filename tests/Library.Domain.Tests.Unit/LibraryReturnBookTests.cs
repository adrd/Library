using FluentAssertions;
using Library.Domain.Factories.Implementation;
using System.Globalization;

namespace Library.Domain.Tests.Unit;

public sealed class LibraryReturnBookTests
{
    [Fact]
    public void Reader_Can_Return_Book_Borrowed_After_Borrowed_Date()
    {
        // arrange
        Library library = this.CreateLibrary();
        Reader reader = this.CreateReader(new ReaderModel() { ReaderName = "Ionut Vasile", CardNumber = "123582" });
        Book book = this.CreateBookToBorrow(new BorrowBookModel() { BookTitle = "Creierul", BookAuthor = "David Eagleman", Isbn = "978-606-92679-1-2" });
        Date borrowDate = new Date(new DateTime(2023, 7, 20));
        library.Borrow(book, reader, borrowDate);

        Date returnDate = new Date(new DateTime(2023, 7, 22));

        // act
        bool actualResult = library.CanReturn(book, reader, returnDate);

        // assert
        actualResult.Should().BeTrue();
    }

    [Fact]
    public void Reader_Cant_Return_Book_Before_Borrowed_Date()
    {
        // arrange
        Library library = this.CreateLibrary();
        Reader reader = this.CreateReader(new ReaderModel() { ReaderName = "Ionut Vasile", CardNumber = "123582" });
        Book book = this.CreateBookToBorrow(new BorrowBookModel() { BookTitle = "Creierul", BookAuthor = "David Eagleman", Isbn = "978-606-92679-1-2" });
        Date borrowDate = new Date(new DateTime(2023, 7, 20));
        library.Borrow(book, reader, borrowDate);

        Date returnDate = new Date(new DateTime(2023, 7, 19));

        // act
        bool actualResult = library.CanReturn(book, reader, returnDate);

        // assert
        actualResult.Should().BeFalse();
    }

    [Fact]
    public void Reader_Return_Book_Within_Two_Weeks_No_Penalty_Is_Requited()
    {
        // arrange
        Library library = this.CreateLibrary();
        Reader reader = this.CreateReader(new ReaderModel() { ReaderName = "Ionut Vasile", CardNumber = "123582" });
        Book book = this.CreateBookToBorrow(new BorrowBookModel() { BookTitle = "Creierul", BookAuthor = "David Eagleman", Isbn = "978-606-92679-1-2" });
        Date borrowDate = new Date(new DateTime(2023, 7, 20));
        library.Borrow(book, reader, borrowDate);

        Date returnDate = new Date(new DateTime(2023, 7, 27));

        // act
        bool actualResult = library.CheckReaderPenalty(book, reader, returnDate);

        // assert
        actualResult.Should().BeFalse();
    }

    [Fact]
    public void Reader_Return_Book_One_Day_After_Two_Weeks_Period_Penalty_Is_Required()
    {
        // arrange
        Library library = this.CreateLibrary();
        Reader reader = this.CreateReader(new ReaderModel() { ReaderName = "Ionut Vasile", CardNumber = "123582" });
        Book book = this.CreateBookToBorrow(new BorrowBookModel() { BookTitle = "Creierul", BookAuthor = "David Eagleman", Isbn = "978-606-92679-1-2" });
        Date borrowDate = new Date(new DateTime(2023, 7, 20));
        library.Borrow(book, reader, borrowDate);

        Date returnDate = new Date(new DateTime(2023, 8, 4));

        // act
        bool actualResult = library.CheckReaderPenalty(book, reader, returnDate);

        // assert
        actualResult.Should().BeTrue();
    }

    [Fact]
    public void Reader_Return_Book_One_Day_After_Two_Weeks_Period_Penalty_Should_Be_One()
    {
        // arrange
        Library library = this.CreateLibrary();
        Reader reader = this.CreateReader(new ReaderModel() { ReaderName = "Ionut Vasile", CardNumber = "123582" });
        Book book = this.CreateBookToBorrow(new BorrowBookModel() { BookTitle = "Creierul", BookAuthor = "David Eagleman", Isbn = "978-606-92679-1-2" });
        Date borrowDate = new Date(new DateTime(2023, 7, 20));
        library.Borrow(book, reader, borrowDate);

        Date returnDate = new Date(new DateTime(2023, 8, 4));

        // act
        Money actualResult = library.ComputeReaderPenalty(book, reader, returnDate);

        // assert
        actualResult.Should().Be(new Money(1));
    }

    [Fact]
    public void Reader_Return_Book_Then_Book_Items_Should_Be_Four()
    {
        // arrange
        Library library = this.CreateLibrary();
        Reader reader = this.CreateReader(new ReaderModel() { ReaderName = "Ionut Vasile", CardNumber = "123582" });
        Book book = this.CreateBookToBorrow(new BorrowBookModel() { BookTitle = "Creierul", BookAuthor = "David Eagleman", Isbn = "978-606-92679-1-2" });
        Date borrowDate = new Date(new DateTime(2023, 7, 20));
        library.Borrow(book, reader, borrowDate);

        Date returnDate = new Date(new DateTime(2023, 8, 4));
        library.Return(book, reader, returnDate);

        // act
        int actualResult = library.NumberOfCopies(book);

        // assert
        actualResult.Should().Be(4);
    }

    [Fact]
    public void Reader_Cant_Return_Book_Without_Borrowing()
    {
        // arrange
        Library library = this.CreateLibrary();
        Reader reader = this.CreateReader(new ReaderModel() { ReaderName = "Ionut Vasile", CardNumber = "123582" });
        Book book = this.CreateBookToBorrow(new BorrowBookModel() { BookTitle = "Creierul", BookAuthor = "David Eagleman", Isbn = "978-606-92679-1-2" });
        Date returnDate = new Date(new DateTime(2023, 8, 4));
        
        // act
        bool actualResult = library.CanReturn(book, reader, returnDate);

        // assert
        actualResult.Should().BeFalse();
    }

    [Fact]
    public void Reader_Cant_Return_Book_When_No_Books_Are_In_Library()
    {
        // arrange
        Library library = new Library();
        Reader reader = this.CreateReader(new ReaderModel() { ReaderName = "Ionut Vasile", CardNumber = "123582" });
        Book book = this.CreateBookToBorrow(new BorrowBookModel() { BookTitle = "Creierul", BookAuthor = "David Eagleman", Isbn = "978-606-92679-1-2" });
        Date returnDate = new Date(new DateTime(2023, 8, 4));

        // act
        bool actualResult = library.CanReturn(book, reader, returnDate);

        // assert
        actualResult.Should().BeFalse();
    }

    public Reader CreateReader(ReaderModel model)
    {
        ReaderFactory readerFactory = new ReaderFactory();

        Reader reader = readerFactory.WithName(readerNameFactory => readerNameFactory
                                         .With(model.ReaderName))
                                     .WithCard(cardFactory => cardFactory
                                         .WithNumber(int.Parse(model.CardNumber)))
                                     .Build();

        return reader;
    }

    public class ReaderModel
    {
        public string ReaderName { get; set; }

        public string CardNumber { get; set; }
    }

    private Library CreateLibrary()
    {
        Library library = new Library();

        Book book1 = this.CreateBook(new AddBookModel() { BookTitle = "Creierul", BookAuthor = "David Eagleman", Isbn = "978-606-92679-1-2", RentalPrice = "100" });
        Book book2 = this.CreateBook(new AddBookModel() { BookTitle = "Capra cu trei iezi", BookAuthor = "Ion Creanga", Isbn = "978-906-92679-1-2", RentalPrice = "45.23" });
        Book book3 = this.CreateBook(new AddBookModel() { BookTitle = "Poezii", BookAuthor = "Mihai Eminescu", Isbn = "978-906-42679-1-2", RentalPrice = "87.06" });
        Book book4 = this.CreateBook(new AddBookModel() { BookTitle = "Moara cu Noroc", BookAuthor = "Ioan Slavici", Isbn = "978-906-42379-1-2", RentalPrice = "63.51" });
        Book book5 = this.CreateBook(new AddBookModel() { BookTitle = "Amintiri din Copilarie", BookAuthor = "Ion Creanga", Isbn = "978-906-42378-1-2", RentalPrice = "75.03" });

        library.Add(book1);
        library.Add(book2);
        library.Add(book3);
        library.Add(book4);
        library.Add(book5);

        library.Add(book1);
        library.Add(book3);
        library.Add(book4);

        library.Add(book1);
        library.Add(book2);
        library.Add(book4);

        library.Add(book1);

        return library;
    }

    private Book CreateBook(AddBookModel model)
    {
        BookFactory bookFactory = new BookFactory();

        Book book = bookFactory.WithName(bookNameFactory => bookNameFactory
                                   .WithTitle(model.BookTitle)
                                   .WithAuthor(model.BookAuthor))
                               .WithIsbn(isbnFactory => isbnFactory
                                   .With(model.Isbn))
                               .WithRentalPrice(rentalPriceFactory => rentalPriceFactory
                                   .WithPrice(moneyFactory => moneyFactory
                                       .WithAmount(decimal.Parse(model.RentalPrice, CultureInfo.InvariantCulture))
                                   ))
                               .Build();

        return book;
    }

    private sealed class AddBookModel
    {
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public string Isbn { get; set; }
        public string RentalPrice { get; set; }
    }

    private Book CreateBookToBorrow(BorrowBookModel model)
    {
        BookFactory bookFactory = new BookFactory();

        Book book = bookFactory.WithName(bookName => bookName
                                    .WithTitle(model.BookTitle)
                                    .WithAuthor(model.BookAuthor))
                                .WithIsbn(bookIsbn => bookIsbn
                                    .With(model.Isbn))
                                .WithRentalPrice(rentalFactory => rentalFactory
                                    .WithPrice(moneyFactory => moneyFactory
                                        .WithAmount(0)
                                    ))
                                .Build();

        return book;
    }

    private sealed class BorrowBookModel
    {
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public string Isbn { get; set; }
    }
}

