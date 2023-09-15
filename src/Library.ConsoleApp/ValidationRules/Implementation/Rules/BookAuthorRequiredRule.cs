using Library.ConsoleApp.Model;

namespace Library.ConsoleApp.ValidationRules.Implementation.Rules;

public sealed class BookAuthorRequiredRule : IInputModelRule<AddBookInputModel>, IInputModelRule<NumberOfCopiesOfABookInputModel>,
                                             IInputModelRule<BorrowBookInputModel>, IInputModelRule<ReturnBookInputModel>
{
    public bool CompliesWithRule(AddBookInputModel inputModel)
        => this.IsValid(inputModel.BookAuthor);

    public bool CompliesWithRule(NumberOfCopiesOfABookInputModel inputModel)
        => this.IsValid(inputModel.BookAuthor);

    public bool CompliesWithRule(BorrowBookInputModel inputModel)
        => this.IsValid(inputModel.BookAuthor);

    public bool CompliesWithRule(ReturnBookInputModel inputModel)
        => this.IsValid(inputModel.BookAuthor);

    private bool IsValid(string bookAuthor)
    {
        if (string.IsNullOrEmpty(bookAuthor) || string.IsNullOrWhiteSpace(bookAuthor))
            return false;

        return true;
    }

    public string ErrorMessage
        => "Autorul cartii este obligatoriu";
}