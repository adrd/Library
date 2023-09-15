using FluentAssertions;
using Library.ConsoleApp.Model;
using Library.ConsoleApp.ValidationRules.Implementation.Rules;

namespace Library.ConsoleApp.Tests.Unit;

public sealed class ReturnDateFormatRuleTests
{
    [Theory]
    [InlineData("20.06.2023", true)]
    [InlineData("20.061.2023", false)]
    [InlineData("1.06.2023", true)]
    [InlineData("1.6.2023", true)]
    [InlineData("1.6.202", false)]
    [InlineData("12.06.2021", true)]
    [InlineData("1a.06.2021", false)]
    [InlineData("10-06.2021", false)]
    [InlineData("10,06.2021", false)]
    [InlineData("22.6.2021", true)]
    [InlineData("22.6-2021", false)]
    [InlineData("22.6-b021", false)]
    public void Check_Return_Date_Format(string returnDate, bool expectedResult)
    {
        // arrange
        ReturnBookInputModel inputModel = this.CreateInputModel(returnDate);

        // act
        ReturnDateFormatRule returnDateFormatRule = new ReturnDateFormatRule();
        bool actualResult = returnDateFormatRule.CompliesWithRule(inputModel);

        // assert
        actualResult.Should().Be(expectedResult);
    }

    private ReturnBookInputModel CreateInputModel(string returnDate)
    {
        ReturnBookInputModel inputModel = new ReturnBookInputModel();
        inputModel.BookTitle = string.Empty;
        inputModel.BookAuthor = string.Empty;
        inputModel.Isbn = string.Empty;
        inputModel.ReaderName = string.Empty;
        inputModel.CardNumber = string.Empty;
        inputModel.ReturnDate = returnDate;

        return inputModel;
    }
}