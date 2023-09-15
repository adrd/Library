namespace Library.Domain;

public sealed class Book
{
    private readonly BookName _bookName;
    private readonly Isbn _isbn;
    private readonly Price _rentalPrice;

    public Book(BookName bookName, Isbn isbn, Price rentalPrice)
    {
        this._bookName = bookName;
        this._isbn = isbn;
        this._rentalPrice = rentalPrice;
    }

    public BorrowedBook Create(Date borrowDate)
    {
        BorrowedBook borrowedBook = new BorrowedBook(this._bookName, this._isbn, this._rentalPrice, borrowDate,borrowDate.Add(14));
            
        return borrowedBook;
    }

    #region Equals
    private bool Equals(Book other)
    {
        return this._bookName.Equals(other._bookName) && this._isbn.Equals(other._isbn);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is Book other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this._bookName, this._isbn);
    }
    #endregion

    public override string ToString()
    {
        return $"{this._bookName} ({this._isbn}) - Pret Inchiriere : {this._rentalPrice}";
    }
}