using System.Globalization;
using FluentAssertions;
using Library.Domain.Factories.Implementation;

namespace Library.Domain.Tests.Unit;

public sealed class LibraryBorrowBookTests
{
    [Fact]
    public void Can_Borrow_Book_Item_Exists_In_Library_And_Reader_Didnt_Borrow_It()
    {
        // arrange
        Library library = this.CreateLibrary();
        Reader reader = this.CreateReader(new ReaderModel() { ReaderName = "Ionut Vasile", CardNumber = "123582" });
        Book book = this.CreateBookToBorrow(new BorrowBookModel() { BookTitle = "Creierul", BookAuthor = "David Eagleman", Isbn = "978-606-92679-1-2"});

        // act
        bool actualResult = library.CanBorrow(book, reader);

        // assert
        actualResult.Should().BeTrue();
    }

    [Fact]
    public void Can_Borrow_Book_Item_Already_Borrowed_By_Reader()
    {
        // arrange
        Library library = this.CreateLibrary();
        Reader reader = this.CreateReader(new ReaderModel() { ReaderName = "Ionut Vasile", CardNumber = "123582" });
        Book book = this.CreateBookToBorrow(new BorrowBookModel() { BookTitle = "Amintiri din Copilarie", BookAuthor = "Ion Creanga", Isbn = "978-906-42378-1-2" });
        Date borrowDate = new Date(new DateTime(2023, 7, 20));
        library.Borrow(book, reader, borrowDate);

        // act
        bool actualResult = library.CanBorrow(book, reader);

        // assert
        actualResult.Should().BeFalse();
    }

    [Fact]
    public void Can_Borrow_Book_Item_Already_Borrowed_By_Another_Reader()
    {
        // arrange
        Library library = this.CreateLibrary();
        Reader reader1 = this.CreateReader(new ReaderModel() { ReaderName = "Ionut Vasile", CardNumber = "123582" });
        Book book = this.CreateBookToBorrow(new BorrowBookModel() { BookTitle = "Amintiri din Copilarie", BookAuthor = "Ion Creanga", Isbn = "978-906-42378-1-2" });
        Date borrowDate = new Date(new DateTime(2023, 7, 20));
        library.Borrow(book, reader1, borrowDate);

        Reader reader2 = this.CreateReader(new ReaderModel() { ReaderName = "Marin Popescu", CardNumber = "123583" });

        // act
        bool actualResult = library.CanBorrow(book, reader2);

        // assert
        actualResult.Should().BeFalse();
    }

    [Fact]
    public void Book_Item_Borrowed_By_Two_Different_Readers()
    {
        // assert
        Library library = this.CreateLibrary();
        Reader reader1 = this.CreateReader(new ReaderModel() { ReaderName = "Ionut Vasile", CardNumber = "123582" });
        Reader reader2 = this.CreateReader(new ReaderModel() { ReaderName = "Marin Popescu", CardNumber = "123583" });

        Book book = this.CreateBookToBorrow(new BorrowBookModel() { BookTitle = "Creierul", BookAuthor = "David Eagleman", Isbn = "978-606-92679-1-2" });

        Date borrowDate = new Date(new DateTime(2023, 7, 20));

        library.Borrow(book, reader1, borrowDate);
        library.Borrow(book, reader2, borrowDate);

        Reader reader3 = this.CreateReader(new ReaderModel() { ReaderName = "Radu Constantin", CardNumber = "123083" });

        // act
        bool actualResult = library.CanBorrow(book, reader3);

        // arrange
        actualResult.Should().BeTrue();
    }

    [Fact]
    public void Can_Borrow_Book_Item_Does_Not_Exists_In_Library()
    {
        // arrange
        Library library = this.CreateLibrary();
        Reader reader = this.CreateReader(new ReaderModel() { ReaderName = "Ionut Vasile", CardNumber = "123582" });
        Book book = this.CreateBookToBorrow(new BorrowBookModel() { BookTitle = "Programare C#", BookAuthor = "Tudor Sorin", Isbn = "978-606-92679-1-0" });

        // act
        bool actualResult = library.CanBorrow(book, reader);

        // assert
        actualResult.Should().BeFalse();
    }

    [Fact]
    public void Cant_Borrow_Book_Item_When_No_Books_Are_In_Library()
    {
        // arrange
        Library library = new Library();
        Reader reader = this.CreateReader(new ReaderModel() { ReaderName = "Ionut Vasile", CardNumber = "123582" });
        Book book = this.CreateBookToBorrow(new BorrowBookModel() { BookTitle = "Programare C#", BookAuthor = "Tudor Sorin", Isbn = "978-606-92679-1-0" });
        
        // act
        bool actualResult = library.CanBorrow(book, reader);

        // assert
        actualResult.Should().BeFalse();
    }

    public Reader CreateReader(ReaderModel model)
    {
        ReaderFactory readerFactory = new ReaderFactory();

        Reader reader = readerFactory.WithName(readerName => readerName
                .With(model.ReaderName))
            .WithCard(card => card
                .WithNumber(int.Parse(model.CardNumber)))
            .Build();

        return reader;
    }

    public class ReaderModel
    {
        public string ReaderName { get; set; }

        public string CardNumber { get; set; }
    }

    public Library CreateLibrary()
    {
        Library library = new Library();

        Book book1 = this.CreateBook(new AddBookModel() { BookTitle = "Creierul", BookAuthor = "David Eagleman", Isbn = "978-606-92679-1-2", RentalPrice = "100.99" });
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

        Book book = bookFactory.WithName(bookName => bookName
                                   .WithTitle(model.BookTitle)
                                   .WithAuthor(model.BookAuthor))
                               .WithIsbn(bookIsbn => bookIsbn
                                   .With(model.Isbn))
                               .WithRentalPrice(rentalFactory => rentalFactory
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

        Book book = bookFactory.WithName(bookNameFactory => bookNameFactory
                                    .WithTitle(model.BookTitle)
                                    .WithAuthor(model.BookAuthor))
                                .WithIsbn(isbnFactory => isbnFactory
                                    .With(model.Isbn))
                                .WithRentalPrice(rentalPriceFactory => rentalPriceFactory
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