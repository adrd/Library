using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Library.ConsoleApp.Model;
using Library.ConsoleApp.ValidationRules.Implementation.Rules;

namespace Library.ConsoleApp.Tests.Unit
{
    public sealed class RentalPriceValidTests
    {
        [Theory]
        [InlineData("456,90", true)]
        [InlineData("45.99", true)]
        [InlineData("60", true)]
        [InlineData("89a", false)]
        [InlineData(" 45.99", true)]
        [InlineData(" 45.99 ", true)]
        public void Check_Rental_Price_If_Is_Valid(string price, bool expectedResult)
        {
            // arrange
            AddBookInputModel inputModel = this.CreateInputModel(price);

            // act
            RentalPriceValidRule rentalPriceValidRule = new RentalPriceValidRule();
            bool actualResult = rentalPriceValidRule.CompliesWithRule(inputModel);

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
}
