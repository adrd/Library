namespace Library.ConsoleApp.ValidationRules;

public interface IInputModelRule<TInputModel>
{
    bool CompliesWithRule(TInputModel inputModel);

    string ErrorMessage { get; }
}