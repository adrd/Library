using FluentAssertions;

namespace Library.Domain.Tests.Unit;

public sealed class BorrowedBookTests
{
    [Theory]
    [InlineData(2023, 7, 19, true)]
    [InlineData(2023, 7, 18, false)]
    [InlineData(2023, 7, 17, false)]
    public void Borrow_Book_Has_Penalty(int year, int month, int day, bool expectedResult)
    {
        // arrange
        BorrowedBook bb = this.Create();

        // act
        Date returnDate = new Date(new DateTime(year, month, day));
        bool actualResult = bb.HasPenalty(returnDate);

        // assert
        actualResult.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(2023, 7, 19, 0.48)]
    [InlineData(2023, 7, 20, 0.96)]
    [InlineData(2023, 7, 17, 0)]
    public void Borrow_Book_Penalty(int year, int month, int day, decimal expectedResult)
    {
        // arrange
        BorrowedBook bb = this.Create();

        // act
        Date returnDate = new Date(new DateTime(year, month, day));
        decimal actualResult = bb.ComputePenalty(returnDate);

        // assert
        actualResult.Should().Be(expectedResult);
    }

    private BorrowedBook Create()
    {
        BorrowedBook bb = new BorrowedBook(new BookName("Creierul", "David Eagleman"), new Isbn("978-606-92679-1-2"),
            new Price(new Money(47.99m)), new Date(new DateTime(2023, 7, 4)), new Date(new DateTime(2023, 7, 18)));

        return bb;
    }
}