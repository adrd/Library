using Library.ConsoleApp.Model;

namespace Library.ConsoleApp.ValidationRules.Implementation.Rules;

public class CardRequiredRule : IInputModelRule<BorrowBookInputModel>, IInputModelRule<ReturnBookInputModel>
{
    public bool CompliesWithRule(BorrowBookInputModel inputModel)
        => this.IsValid(inputModel.CardNumber);

    public bool CompliesWithRule(ReturnBookInputModel inputModel)
        => this.IsValid(inputModel.CardNumber);

    private bool IsValid(string cardNumber)
    {
        if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrWhiteSpace(cardNumber))
            return false;

        return true;
    }

    public string ErrorMessage
        => "Cardul este obligatoriu";
}