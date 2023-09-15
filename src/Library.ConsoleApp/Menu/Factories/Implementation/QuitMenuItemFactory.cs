using Library.ConsoleApp.Commands.Implementation;

namespace Library.ConsoleApp.Menu.Factories.Implementation;

internal sealed class QuitMenuItemFactory : IMenuItemFactory
{
    public MenuItem Create()
    {
        return new MenuItem("6. Iesire din aplicatie", '6', true, true, new DoNothingCommand());
    }
}