namespace Library.Domain.Factories.Implementation;

public class BookNameFactory : IBookNameFactory
{
    private string _title;
    private string _author;

    public BookNameFactory()
    {
        this._title = string.Empty;
        this._author = string.Empty;
    }

    public IBookNameFactory WithTitle(string title)
    {
        this._title = title;
        return this;
    }

    public IBookNameFactory WithAuthor(string author)
    {
        this._author = author;
        return this;
    }

    public BookName Build()
        => new BookName(this._title, this._author);


}