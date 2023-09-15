namespace Library.Domain.Factories.Implementation;

public class IsbnFactory : IIsbnFactory
{
    private string _isbn;

    public IsbnFactory()
    {
        this._isbn = string.Empty;
    }

    public IIsbnFactory With(string isbn)
    {
        this._isbn = isbn;
        return this;
    }

    public Isbn Build()
        => new Isbn(this._isbn);
}