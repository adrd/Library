using Library.ConsoleApp.Model;
using System.Text.RegularExpressions;

namespace Library.ConsoleApp.ValidationRules.Implementation.Rules;

public sealed class CardFormatRule : IInputModelRule<BorrowBookInputModel>, IInputModelRule<ReturnBookInputModel>
{
    private readonly string cardRegex = @"^[0-9]{6}$";

    public bool CompliesWithRule(BorrowBookInputModel inputModel)
        => this.IsValid(inputModel.CardNumber.Trim());

    public bool CompliesWithRule(ReturnBookInputModel inputModel)
        => this.IsValid(inputModel.CardNumber.Trim());

    private bool IsValid(string cardNumber)
    {
        Regex re = new Regex(cardRegex);

        if (!re.IsMatch(cardNumber))
            return false;

        return true;
    }

    public string ErrorMessage
        => "Formatul cardului este invalid (exemplu de format: 123456)";
}