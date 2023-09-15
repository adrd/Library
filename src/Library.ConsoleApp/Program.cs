using Library.ConsoleApp.Menu.Factories;
using Library.ConsoleApp.UI.Implementation;

namespace Library.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuFactory menuFactory 
                = new MenuFactory(new Domain.Library());   

            UserInterface userInterface = new UserInterface(menuFactory);

            while (userInterface.ReadCommand())
            {
                userInterface.ExecuteCommand();
            }
        }
    }
}