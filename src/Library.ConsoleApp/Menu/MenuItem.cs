using Library.ConsoleApp.Commands;

namespace Library.ConsoleApp.Menu;

internal sealed class MenuItem
{
    private readonly string _name;
    private readonly char _shortcutKey;
    private readonly bool _isQuit;
    private readonly bool _isVisible;
    private readonly ICommand _command;

    public MenuItem(string name, char shortcutKey, bool isQuit, bool isVisible, ICommand command)
    {
        this._name = name;
        this._shortcutKey = shortcutKey;
        this._isQuit = isQuit;
        this._isVisible = isVisible;
        this._command = command;
    }

    public bool IsQuitCommand
        => this._isQuit;

    public ICommand Command
        => this._command;

    public void Display()
    {
        if (!this._isVisible)
            return;

        Console.Write(this._name);
        Console.WriteLine();
    }

    public bool MatchesKey(char key)
    {
        return char.ToLower(this._shortcutKey) == char.ToLower(key);
    }
}