using Library.ConsoleApp.Model;

namespace Library.ConsoleApp.ValidationRules.Implementation.Rules;

public sealed class RentalPriceRequiredRule : IInputModelRule<AddBookInputModel>
{
    public bool CompliesWithRule(AddBookInputModel inputModel)
    {
        if (string.IsNullOrEmpty(inputModel.RentalPrice) || string.IsNullOrWhiteSpace(inputModel.RentalPrice))
            return false;

        return true;
    }

    public string ErrorMessage
        => "Pretul de inchiriere este obligatoriu";
}