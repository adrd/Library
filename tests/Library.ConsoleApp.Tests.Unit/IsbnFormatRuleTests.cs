using FluentAssertions;
using Library.ConsoleApp.Model;
using Library.ConsoleApp.ValidationRules.Implementation.Rules;

namespace Library.ConsoleApp.Tests.Unit
{
    public sealed class IsbnFormatRuleTests
    {
        [Theory]
        [InlineData("978-606-92679-1-2", true)]
        [InlineData(" 978-606-92679-1-2", true)]
        [InlineData(" 978-606-92679-1-2 ", true)]
        [InlineData("973-99377-6-4", false)]
        [InlineData(" 973-99377-6-4", false)]
        [InlineData(" 973-99377-6-4 ", false)]
        [InlineData("1234567890112", false)]
        [InlineData("978-973-46-0815-7", true)]
        [InlineData("978-973-8171-96-1", true)]
        [InlineData("a-123-edccd-123-1", false)]
        [InlineData("", false)]
        public void Check_Isbn_Format(string isbn, bool expectedResult)
        {
            // arrange
            AddBookInputModel inputModel = this.CreateInputModel(isbn);

            // act
            IsbnFormatRule isbnFormatRule = new IsbnFormatRule();
            bool result = isbnFormatRule.CompliesWithRule(inputModel);

            // assert
            result.Should().Be(expectedResult);
        }

        private AddBookInputModel CreateInputModel(string isbn)
        {
            AddBookInputModel inputModel = new AddBookInputModel();
            inputModel.BookTitle = string.Empty;
            inputModel.Isbn = isbn;
            inputModel.RentalPrice = string.Empty;

            return inputModel;
        }
    }
}