namespace Library.Domain;

public record ReaderName
{
    private readonly string _name;

    public ReaderName(string name)
    {
        this._name = name;
    }

    public override string ToString()
    {
        return this._name;
    }
}