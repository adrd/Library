namespace Library.Domain.Factories;

public interface IBookNameFactory
{
    IBookNameFactory WithTitle(string title);

    IBookNameFactory WithAuthor(string author);
}