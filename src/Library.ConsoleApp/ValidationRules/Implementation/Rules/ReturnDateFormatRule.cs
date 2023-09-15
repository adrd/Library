using System.Text.RegularExpressions;
using Library.ConsoleApp.Model;

namespace Library.ConsoleApp.ValidationRules.Implementation.Rules;

public sealed class ReturnDateFormatRule : IInputModelRule<ReturnBookInputModel>
{
    private readonly string dateRegex = @"^[0-9]{1,2}\.[0-9]{1,2}\.[0-9]{4}$";

    public bool CompliesWithRule(ReturnBookInputModel inputModel)
    {
        Regex re = new Regex(dateRegex);

        if (!re.IsMatch(inputModel.ReturnDate.Trim()))
            return false;

        return true;
    }

    public string ErrorMessage
        => "Data returnarii cartii nu este in formatul corect (format data: zz.ll.aaaa)";
}