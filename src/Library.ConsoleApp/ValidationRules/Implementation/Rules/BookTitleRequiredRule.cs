using Library.ConsoleApp.Model;

namespace Library.ConsoleApp.ValidationRules.Implementation.Rules;

public sealed class BookTitleRequiredRule : IInputModelRule<AddBookInputModel>, IInputModelRule<BorrowBookInputModel>, IInputModelRule<ReturnBookInputModel>,
                                     IInputModelRule<NumberOfCopiesOfABookInputModel>
{
    public bool CompliesWithRule(AddBookInputModel inputModel)
        => this.IsValid(inputModel.BookTitle);

    public bool CompliesWithRule(BorrowBookInputModel inputModel)
        => this.IsValid(inputModel.BookTitle);

    public bool CompliesWithRule(ReturnBookInputModel inputModel)
        => this.IsValid(inputModel.BookTitle);

    public bool CompliesWithRule(NumberOfCopiesOfABookInputModel inputModel)
        => this.IsValid(inputModel.BookTitle);

    private bool IsValid(string bookTitle)
    {
        if (string.IsNullOrEmpty(bookTitle) || string.IsNullOrWhiteSpace(bookTitle))
            return false;

        return true;
    }

    public string ErrorMessage
        => "Titlul cartii este obligatoriu";
}