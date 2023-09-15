using Library.ConsoleApp.Model;

namespace Library.ConsoleApp.ValidationRules.Implementation.Processor;

public sealed class NumberOfCopiesOfABookRuleProcessor : IInputModelRuleProcessor<NumberOfCopiesOfABookInputModel>
{
    private readonly IEnumerable<IInputModelRule<NumberOfCopiesOfABookInputModel>> _inputModelRules;

    public NumberOfCopiesOfABookRuleProcessor(IEnumerable<IInputModelRule<NumberOfCopiesOfABookInputModel>> inputModelRules)
    {
        this._inputModelRules = inputModelRules;
    }

    public (bool, IEnumerable<string>) PassesAllRules(NumberOfCopiesOfABookInputModel inputModel)
    {
        bool passedRules = true;
        List<string> errors = new List<string>();

        foreach (IInputModelRule<NumberOfCopiesOfABookInputModel> inputModelRule in this._inputModelRules)
        {
            if (!inputModelRule.CompliesWithRule(inputModel))
            {
                errors.Add(inputModelRule.ErrorMessage);
                passedRules = false;
            }
        }

        return (passedRules, errors);
    }
}