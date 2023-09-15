using FluentAssertions;
using Library.ConsoleApp.Model;
using Library.ConsoleApp.ValidationRules.Implementation.Rules;

namespace Library.ConsoleApp.Tests.Unit;

public sealed class RentalPriceFormatRuleTests
{
    [Theory]
    [InlineData("45.99", true)]
    [InlineData("45.991", false)]
    [InlineData("100", true)]
    [InlineData("123a.56", false)]
    [InlineData("45.9", true)]
    [InlineData("12,77", false)]
    [InlineData("89.00", true)]
    [InlineData("-89.00", false)]
    [InlineData("-1", false)]
    [InlineData("0", true)]
    [InlineData("248.10", true)]
    public void Check_Rental_Price_Format(string price, bool expectedResult)
    {
        // arrange
        AddBookInputModel inputModel = this.CreateInputModel(price);

        // act
        RentalPriceFormatRule rentalPriceFormatRule = new RentalPriceFormatRule();
        bool actualResult = rentalPriceFormatRule.CompliesWithRule(inputModel);

        // assert
        actualResult.Should().Be(expectedResult);
    }

    private AddBookInputModel CreateInputModel(string price)
    {
        AddBookInputModel inputModel = new AddBookInputModel();
        inputModel.BookTitle = string.Empty;
        inputModel.Isbn = string.Empty;
        inputModel.RentalPrice = price;

        return inputModel;
    }
}