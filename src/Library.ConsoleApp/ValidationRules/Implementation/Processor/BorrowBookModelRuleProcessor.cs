using Library.ConsoleApp.Model;

namespace Library.ConsoleApp.ValidationRules.Implementation.Processor;

public sealed class BorrowBookModelRuleProcessor : IInputModelRuleProcessor<BorrowBookInputModel>
{
    private readonly IEnumerable<IInputModelRule<BorrowBookInputModel>> _inputModelRules;

    public BorrowBookModelRuleProcessor(IEnumerable<IInputModelRule<BorrowBookInputModel>> inputModelRules)
    {
        this._inputModelRules = inputModelRules.ToList();
    }

    public (bool, IEnumerable<string>) PassesAllRules(BorrowBookInputModel inputModel)
    {
        bool passedRules = true;
        List<string> errors = new List<string>();

        foreach (IInputModelRule<BorrowBookInputModel> inputModelRule in this._inputModelRules)
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