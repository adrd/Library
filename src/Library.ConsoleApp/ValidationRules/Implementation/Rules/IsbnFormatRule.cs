using System.Text.RegularExpressions;
using Library.ConsoleApp.Model;

namespace Library.ConsoleApp.ValidationRules.Implementation.Rules;

/*
  ^
  (?:ISBN(?:-13)?:?\ )?     # Optional ISBN/ISBN-13 identifier.
  (?=                       # Basic format pre-checks (lookahead):
    [0-9]{13}$              #   Require 13 digits (no separators).
   |                        #  Or:
    (?=(?:[0-9]+[-\ ]){4})  #   Require 4 separators
    [-\ 0-9]{17}$           #     out of 17 characters total.
  )                         # End format pre-checks.
  97[89][-\ ]?              # ISBN-13 prefix.
  [0-9]{1,5}[-\ ]?          # 1-5 digit group identifier.
  [0-9]+[-\ ]?[0-9]+[-\ ]?  # Publisher and title identifiers.
  [0-9]                     # Check digit.
  $
 */

public sealed class IsbnFormatRule : IInputModelRule<AddBookInputModel>, IInputModelRule<BorrowBookInputModel>, IInputModelRule<ReturnBookInputModel>,
                              IInputModelRule<NumberOfCopiesOfABookInputModel>
{
    private readonly string isbn13Regex = @"^(?:ISBN(?:-13)?:?)?(?=[0-9]{13}$|(?=(?:[0-9]+[-]){4})[-0-9]{17}$)97[89][-]?[0-9]{1,5}[-]?[0-9]+[-]?[0-9]+[-]?[0-9]$";

    public bool CompliesWithRule(AddBookInputModel inputModel)
        => this.IsValid(inputModel.Isbn.Trim());

    public bool CompliesWithRule(BorrowBookInputModel inputModel)
        => this.IsValid(inputModel.Isbn.Trim());

    public bool CompliesWithRule(ReturnBookInputModel inputModel)
        => this.IsValid(inputModel.Isbn.Trim());

    public bool CompliesWithRule(NumberOfCopiesOfABookInputModel inputModel)
        => this.IsValid(inputModel.Isbn.Trim());

    private bool IsValid(string isbn)
    {
        Regex re = new Regex(isbn13Regex);

        if (!re.IsMatch(isbn))
            return false;

        return true;
    }

    public string ErrorMessage
        => "Formatul Isbn-ului este invalid, trebuie sa fie in format de 13 cifre (exemplu: 978-606-92679-1-2)";
}