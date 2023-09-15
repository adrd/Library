namespace Library.Domain.Contracts;

public interface IBorrowBook
{
    bool CanBorrow(Book book, Reader reader);

    void Borrow(Book book, Reader reader, Date borrowDate);
}