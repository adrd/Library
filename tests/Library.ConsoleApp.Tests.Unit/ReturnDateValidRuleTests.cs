using FluentAssertions;
using Library.ConsoleApp.Model;
using Library.ConsoleApp.ValidationRules.Implementation.Rules;

namespace Library.ConsoleApp.Tests.Unit;

public sealed class ReturnDateValidRuleTests
{
    [Theory]
    [InlineData("20.12.2022", true)]
    [InlineData("32.12.2022", false)]
    [InlineData("29.13.2022", false)]
    [InlineData("29.0a.2023", false)]
    [InlineData("29.02.2023", false)]
    [InlineData("29.2.2023", false)]
    [InlineData("0.02.2023", false)]
    [InlineData("28.02.2023", true)]
    [InlineData("28.2.2023", true)]
    [InlineData("28.2-2023", false)]
    [InlineData("31.082.2023", false)]
    [InlineData("32.08.2023", false)]
    [InlineData("321.08.2023", false)]
    [InlineData("31.08.20237", false)]
    [InlineData("31.08.202", false)]
    [InlineData("0.0.0000", false)]
    [InlineData("a1.01.2023", false)]
    [InlineData("32.06.2023", false)]
    [InlineData("00.06.2023", false)]
    [InlineData("13.02.2023", true)]
    public void Check_Return_Date_If_Is_Valid(string returnDate, bool expectedResult)
    {
        // assert
        ReturnBookInputModel inputModel = this.CreateInputModel(returnDate);

        // arrange
        ReturnDateValidRule returnDateValidRule = new ReturnDateValidRule();
        bool actualResult = returnDateValidRule.CompliesWithRule(inputModel);

        // act
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