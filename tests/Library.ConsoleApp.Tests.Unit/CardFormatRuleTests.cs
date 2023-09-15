using FluentAssertions;
using Library.ConsoleApp.Model;
using Library.ConsoleApp.ValidationRules.Implementation.Rules;

namespace Library.ConsoleApp.Tests.Unit;

public sealed class CardFormatRuleTests
{
    [Theory]
    [InlineData("", false)]
    [InlineData("1", false)]
    [InlineData("12", false)]
    [InlineData("123", false)]
    [InlineData("1234", false)]
    [InlineData("12345", false)]
    [InlineData("123456", true)]
    [InlineData("1234567", false)]
    [InlineData("012345", true)]
    [InlineData("123a57", false)]
    [InlineData("-44323", false)]
    [InlineData("123956 ", true)]
    [InlineData(" 123956 ", true)]
    [InlineData(" 123 956 ", false)]
    public void Check_Card_Format(string cardNumber, bool expectedResult)
    {
        // arrange
        BorrowBookInputModel inputModel = this.CreateInputModel(cardNumber);

        // act
        CardFormatRule cardFormatRule = new CardFormatRule();
        bool actualResult = cardFormatRule.CompliesWithRule(inputModel);

        // assert
        actualResult.Should().Be(expectedResult);
    }

    private BorrowBookInputModel CreateInputModel(string cardNumber)
    {
        BorrowBookInputModel inputModel = new BorrowBookInputModel();
        inputModel.BookTitle = string.Empty;
        inputModel.BookAuthor = string.Empty;
        inputModel.Isbn = string.Empty;
        inputModel.ReaderName = string.Empty;
        inputModel.CardNumber = cardNumber;

        return inputModel;
    }
}