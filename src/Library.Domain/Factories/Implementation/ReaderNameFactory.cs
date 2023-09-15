namespace Library.Domain.Factories.Implementation;

public class ReaderNameFactory : IReaderNameFactory
{
    private string _name;

    public ReaderNameFactory()
    {
        this._name = string.Empty;
    }

    public IReaderNameFactory With(string name)
    {
        this._name = name;
        return this;
    }
    
    public ReaderName Build()
        => new ReaderName(this._name);
}