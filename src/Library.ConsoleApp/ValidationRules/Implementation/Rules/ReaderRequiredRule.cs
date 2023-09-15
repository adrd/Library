using Library.ConsoleApp.Model;

namespace Library.ConsoleApp.ValidationRules.Implementation.Rules;

public sealed class ReaderRequiredRule : IInputModelRule<BorrowBookInputModel>, IInputModelRule<ReturnBookInputModel>
{
    public bool CompliesWithRule(BorrowBookInputModel inputModel)
        => this.IsValid(inputModel.ReaderName);

    public bool CompliesWithRule(ReturnBookInputModel inputModel)
        => this.IsValid(inputModel.ReaderName);

    private bool IsValid(string readerName)
    {
        if (string.IsNullOrEmpty(readerName) || string.IsNullOrWhiteSpace(readerName))
            return false;

        return true;
    }

    public string ErrorMessage
        => "Cititorul este obligatoriu";
}