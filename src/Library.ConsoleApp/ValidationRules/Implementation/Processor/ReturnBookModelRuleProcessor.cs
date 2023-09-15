using Library.ConsoleApp.Model;

namespace Library.ConsoleApp.ValidationRules.Implementation.Processor;

public sealed class ReturnBookModelRuleProcessor : IInputModelRuleProcessor<ReturnBookInputModel>
{
    private readonly IEnumerable<IInputModelRule<ReturnBookInputModel>> _inputModelRules;

    public ReturnBookModelRuleProcessor(IEnumerable<IInputModelRule<ReturnBookInputModel>> inputModelRules)
    {
        this._inputModelRules = inputModelRules;
    }

    public (bool, IEnumerable<string>) PassesAllRules(ReturnBookInputModel inputModel)
    {
        bool passedRules = true;
        List<string> errors = new List<string>();

        foreach (IInputModelRule<ReturnBookInputModel> inputModelRule in this._inputModelRules)
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