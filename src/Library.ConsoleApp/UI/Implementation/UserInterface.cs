using Library.ConsoleApp.Commands;
using Library.ConsoleApp.Commands.Implementation;
using Library.ConsoleApp.Menu;
using Library.ConsoleApp.Menu.Factories;

namespace Library.ConsoleApp.UI.Implementation;

internal class UserInterface : IUserInterface
{
    private readonly IEnumerable<MenuItem> _menu;
    private ICommand _currentCommand = new DoNothingCommand();

    public UserInterface(MenuFactory menuFactory)
    {
        this._menu = menuFactory.Create().ToList();
    }

    private void RefreshDisplay()
    {
        Console.Clear(); 
        
        this.DisplayApplicationName();
        this.ShowMenu();
    }

    private void DisplayApplicationName()
    {
        Console.WriteLine("Sistem de management biblioteca\n");
    }

    private void ShowMenu()
    {
        Console.WriteLine("Selectati o optiune (cifra 1 - 6)");
        Console.WriteLine();

        foreach (MenuItem menuItem in this._menu)
            menuItem.Display();

        Console.WriteLine();
    }

    private MenuItem SelectMenuItem()
    {
        ConsoleKeyInfo key = Console.ReadKey(true);

        if (!this.IsValidKey(key))
            return this._menu.Single(item => item.MatchesKey(' '));

        MenuItem selectedItem = this._menu.Single(item => item.MatchesKey(key.KeyChar));

        return selectedItem;
    }

    private bool IsValidKey(ConsoleKeyInfo keyInfo)
    {
        HashSet<ConsoleKey> validKeys 
            = new HashSet<ConsoleKey>() { ConsoleKey.D1, ConsoleKey.NumPad1, ConsoleKey.D2, ConsoleKey.NumPad2, 
                                          ConsoleKey.D3, ConsoleKey.NumPad3, ConsoleKey.D4, ConsoleKey.NumPad4, ConsoleKey.D5, ConsoleKey.NumPad5,
                                          ConsoleKey.D6, ConsoleKey.NumPad6 };
        
        if (!validKeys.Contains(keyInfo.Key))
            return false;

        return true; 
    }
    
    public bool ReadCommand()
    {
        this.RefreshDisplay();
        
        MenuItem selectedMenuItem = this.SelectMenuItem();

        if (selectedMenuItem.IsQuitCommand)
            return false;

        this._currentCommand = selectedMenuItem.Command;

        return true;
    }

    public void ExecuteCommand()
    {
        this._currentCommand.Execute();

        Console.WriteLine();
        Console.Write("Apasati ENTER pentru a continua...");
        Console.ReadLine();
    }
}
