using Library.ConsoleApp.Commands.Implementation;

namespace Library.ConsoleApp.Menu.Factories.Implementation;

internal class DoNothingMenuItemFactory : IMenuItemFactory
{
    public MenuItem Create()
    {
        return new MenuItem(string.Empty, ' ', false, false, new DoNothingCommand());
    }
}