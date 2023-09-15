using System.Text.RegularExpressions;
using Library.ConsoleApp.Model;

namespace Library.ConsoleApp.ValidationRules.Implementation.Rules;

public sealed class RentalPriceFormatRule : IInputModelRule<AddBookInputModel>
{
    private readonly string priceRegex = @"^\d+\.?\d{0,2}$";

    public bool CompliesWithRule(AddBookInputModel inputModel)
    {
        Regex re = new Regex(priceRegex);

        if (!re.IsMatch(inputModel.RentalPrice.Trim()))
            return false;

        return true;
    }

    public string ErrorMessage
        => "Formatul pretului de inchiriere este invalid (exemplu de format: 45.69)";
}