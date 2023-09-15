namespace Library.ConsoleApp.ValidationRules;

public interface IInputModelRuleProcessor<TInputModel>
{
    (bool, IEnumerable<string>) PassesAllRules(TInputModel inputModel);
}