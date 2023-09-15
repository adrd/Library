using Library.ConsoleApp.Model;

namespace Library.ConsoleApp.ValidationRules.Implementation.Rules;

public sealed class RentalPriceValidRule : IInputModelRule<AddBookInputModel>
{
    public bool CompliesWithRule(AddBookInputModel inputModel)
    {
        if (!decimal.TryParse(inputModel.RentalPrice, out decimal price))
            return false;

        return true;
    }

    public string ErrorMessage
        => "Pretul de inchiriere nu este valid (exemplu de pret valid: 45.69)";
}