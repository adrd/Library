namespace Library.Domain.Factories.Implementation
{
    public class BookFactory : IBookFactory
    {
        private BookName _bookName;
        private Isbn _isbn;
        private Price _rentalPrice;

        public IBookFactory WithName(Action<BookNameFactory> action)
        {
            BookNameFactory bookNameFactory = new BookNameFactory();

            action(bookNameFactory);

            this._bookName = bookNameFactory.Build();

            return this;
        }

        public IBookFactory WithIsbn(Action<IsbnFactory> action)
        {
            IsbnFactory isbnFactory = new IsbnFactory();

            action(isbnFactory);

            this._isbn = isbnFactory.Build();

            return this;
        }

        public IBookFactory WithRentalPrice(Action<RentalPriceFactory> action)
        {
            RentalPriceFactory rentalPriceFactory = new RentalPriceFactory();

            action(rentalPriceFactory);

            this._rentalPrice = rentalPriceFactory.Build();

            return this;
        }

        public Book Build()
            => new Book(this._bookName, this._isbn, this._rentalPrice);
    }
}