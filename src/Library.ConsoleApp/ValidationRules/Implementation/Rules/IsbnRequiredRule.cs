using Library.ConsoleApp.Model;

namespace Library.ConsoleApp.ValidationRules.Implementation.Rules;

public sealed class IsbnRequiredRule : IInputModelRule<AddBookInputModel>, IInputModelRule<BorrowBookInputModel>, IInputModelRule<ReturnBookInputModel>, 
                                       IInputModelRule<NumberOfCopiesOfABookInputModel>
{
    public bool CompliesWithRule(AddBookInputModel inputModel)
        => this.IsValid(inputModel.Isbn);

    public bool CompliesWithRule(BorrowBookInputModel inputModel)
        => this.IsValid(inputModel.Isbn);

    public bool CompliesWithRule(ReturnBookInputModel inputModel)
        => this.IsValid(inputModel.Isbn);

    public bool CompliesWithRule(NumberOfCopiesOfABookInputModel inputModel)
        => this.IsValid(inputModel.Isbn);

    private bool IsValid(string isbn)
    {
        if (string.IsNullOrEmpty(isbn) || string.IsNullOrWhiteSpace(isbn))
            return false;

        return true;
    }

    public string ErrorMessage
        => "ISBN-ul este obligatoriu";
}