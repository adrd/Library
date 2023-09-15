using Library.Domain.Contracts;

namespace Library.Domain
{
    public sealed class Library : IAddBook, IListOfBooks, INumberOfCopies, IBorrowBook, IReturnBook
    {
        private Dictionary<Book, List<Book>> _books = new Dictionary<Book, List<Book>>();

        private Dictionary<Book, Dictionary<Reader, BorrowedBook>> _booksBorrowed =
            new Dictionary<Book, Dictionary<Reader, BorrowedBook>>();
        
        public Library()
        {
        }

        public void Add(Book book)
        {
            if (this.IsBookInLibrary(book))
                this.AddBookItem(book);
            else
                this.AddBook(book);
        }

        private void AddBookItem(Book book)
        {
            this._books[book].Add(book);
        }

        private void AddBook(Book book)
        {
            List<Book> books = new List<Book>();
            books.Add(book);
            this._books.Add(book, books);
        }

        private bool IsBookInLibrary(Book book)
        {
            return this._books.ContainsKey(book);
        }

        public IEnumerable<Book> Books
            => this._books.Keys;

        public int NumberOfCopies(Book book)
        {
            if (this.IsBookInLibrary(book))
                return this._books[book].Count;

            return 0;
        }

        public bool CanBorrow(Book book, Reader reader)
        {
            if (this.IsBookAlreadyBorrowedByReader(book, reader))
                return false;

            if (!this.IsBookInLibrary(book))
                return false;

            if (this.IsBookItemAvailable(book))
                return false;

            return true;
        }

        private bool IsBookItemAvailable(Book book)
        {
            return this._books.ContainsKey(book) && this._books[book].Count < 1;
        }

        private bool IsBookAlreadyBorrowedByReader(Book book, Reader reader)
        {
            return this._booksBorrowed.ContainsKey(book) && this._booksBorrowed[book].ContainsKey(reader);
        }

        public void Borrow(Book book, Reader reader, Date borrowDate)
        {
            if (!this.CanBorrow(book, reader))
                return;

            Book oldBook = this._books[book].First();
            this._books[book].Remove(book);

            if (this.IsBookAlreadyBorrowed(book))
            {
                this.LinkReaderWithBorrowedBook(book, reader, borrowDate, oldBook);
            }
            else
            {
                this.AddBorrowedBook(book, reader, borrowDate, oldBook);
            }
        }

        private void LinkReaderWithBorrowedBook(Book book, Reader reader, Date borrowDate, Book oldBook)
        {
            BorrowedBook borrowedBook = oldBook.Create(borrowDate);
            this._booksBorrowed[book].Add(reader, borrowedBook);
        }

        private void AddBorrowedBook(Book book, Reader reader, Date borrowDate, Book oldBook)
        {
            BorrowedBook bookBorrowed = oldBook.Create(borrowDate);
            var readerBorrowedBook = new Dictionary<Reader, BorrowedBook>();
            readerBorrowedBook.Add(reader, bookBorrowed);
            this._booksBorrowed.Add(book, readerBorrowedBook);
        }

        private bool IsBookAlreadyBorrowed(Book book)
        {
            return this._booksBorrowed.ContainsKey(book);
        }

        public void Return(Book book, Reader reader, Date returnDate)
        {
            if (!this.CanReturn(book, reader, returnDate))
                return;

            this.MakeBookItemAvailable(book, reader);
        }

        private void MakeBookItemAvailable(Book book, Reader reader)
        {
            BorrowedBook borrowedBook = this._booksBorrowed[book][reader];
            this._booksBorrowed[book].Remove(reader);
            Book b = borrowedBook.Create();
            this._books[book].Add(b);
        }

        public bool CanReturn(Book book, Reader reader, Date returnDate)
        {
            if (this.IsBookBorrowedByReader(book, reader) &&
                this.IsBookBorrowedReturnDateGreaterThanBorrowedDate(book, reader, returnDate))
                return true;

            return false;
        }

        private bool IsBookBorrowedReturnDateGreaterThanBorrowedDate(Book book, Reader reader, Date returnDate)
        {
            return this._booksBorrowed[book][reader].CanReturn(returnDate);
        }

        private bool IsBookBorrowedByReader(Book book, Reader reader)
        {
            return this._booksBorrowed.ContainsKey(book) && this._booksBorrowed[book].ContainsKey(reader);
        }

        public bool CheckReaderPenalty(Book book, Reader reader, Date returnDate)
        {
            BorrowedBook borrowedBook = this._booksBorrowed[book][reader];

            if (!borrowedBook.HasPenalty(returnDate))
                return false;

            return true;
        }

        public Money ComputeReaderPenalty(Book book, Reader reader, Date returnDate)
        {
            BorrowedBook borrowedBook = this._booksBorrowed[book][reader];

            Money penalty = borrowedBook.ComputePenalty(returnDate);

            return penalty;
        }
    }
}

