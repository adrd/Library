using System.Globalization;
using Library.ConsoleApp.Model;

namespace Library.ConsoleApp.ValidationRules.Implementation.Rules;

public sealed class ReturnDateValidRule : IInputModelRule<ReturnBookInputModel>
{
    public bool CompliesWithRule(ReturnBookInputModel inputModel)
    {
        if (!DateTime.TryParseExact(inputModel.ReturnDate, "d.M.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime returnDate))
            return false;

        return true;
    }

    public string ErrorMessage
        => "Data returnarii cartii nu este valida (format data: zz.ll.aaaa)";
}