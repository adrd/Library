namespace Library.Domain.Contracts;

public interface IReturnBook
{
    bool CanReturn(Book book, Reader reader, Date returnDate);

    bool CheckReaderPenalty(Book book, Reader reader, Date returnDate);

    Money ComputeReaderPenalty(Book book, Reader reader, Date returnDate);

    void Return(Book book, Reader reader, Date returnDate);
}