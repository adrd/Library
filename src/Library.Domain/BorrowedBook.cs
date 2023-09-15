namespace Library.Domain;

public sealed class BorrowedBook
{
    private readonly BookName _bookName;
    private readonly Isbn _isbn;
    private readonly Price _rentalPrice;
    private readonly Date _borrowDate;
    private readonly Date _dueDate;

    public BorrowedBook(BookName bookName, Isbn isbn, Price rentalPrice, Date borrowDate, Date dueDate)
    {
        this._bookName = bookName;
        this._isbn = isbn;
        this._rentalPrice = rentalPrice;
        this._dueDate = dueDate;
        this._borrowDate = borrowDate;
    }

    public bool CanReturn(Date returnDate)
    {
        if (returnDate <= this._borrowDate)
            return false;

        return true;
    }

    public bool HasPenalty(Date returnDate)
    {
        if (returnDate.Substract(this._dueDate).Days > 0)
            return true;

        return false;
    }

    public Money ComputePenalty(Date returnDate)
    {
        if (!this.HasPenalty(returnDate))
            return 0m;

        int days = returnDate.Substract(this._dueDate).Days;

        Money penalty = Math.Round(this._rentalPrice * days * 1 / 100, 2); 

        return penalty;
    }

    public Book Create()
    {
        Book book = new Book(this._bookName, this._isbn, this._rentalPrice);

        return book;
    }

    public override string ToString()
    {
        return $"{this._bookName} - ({this._isbn}) - Pret Inchiriere : {this._rentalPrice} - Data Inchiriere : {this._borrowDate} - Data Scadenta : {this._dueDate}";
    }
}