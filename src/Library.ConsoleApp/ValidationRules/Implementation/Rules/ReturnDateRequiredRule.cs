using Library.ConsoleApp.Model;

namespace Library.ConsoleApp.ValidationRules.Implementation.Rules;

public sealed class ReturnDateRequiredRule : IInputModelRule<ReturnBookInputModel>
{
    public bool CompliesWithRule(ReturnBookInputModel inputModel)
    {
        if (string.IsNullOrEmpty(inputModel.ReturnDate) || string.IsNullOrWhiteSpace(inputModel.ReturnDate))
            return false;

        return true;
    }

    public string ErrorMessage
        => "Data returnarii cartii este obligatorie";
}