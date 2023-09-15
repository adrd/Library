using Library.ConsoleApp.Model;

namespace Library.ConsoleApp.ValidationRules.Implementation.Processor;

public sealed class AddBookInputModelRuleProcessor : IInputModelRuleProcessor<AddBookInputModel>
{
    private readonly IEnumerable<IInputModelRule<AddBookInputModel>> _inputModelRules;

    public AddBookInputModelRuleProcessor(IEnumerable<IInputModelRule<AddBookInputModel>> inputModelRules)
    {
        this._inputModelRules = inputModelRules.ToList();
    }

    public (bool, IEnumerable<string>) PassesAllRules(AddBookInputModel inputModel)
    {
        bool passedRules = true;
        List<string> errors = new List<string>();

        foreach (IInputModelRule<AddBookInputModel> inputModelRule in this._inputModelRules)
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