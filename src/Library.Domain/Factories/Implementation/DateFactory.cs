namespace Library.Domain.Factories.Implementation;

public class DateFactory : IDateFactory
{
    private DateTime _date;

    public DateFactory()
    {
        
    }

    public IDateFactory With(DateTime date)
    {
        this._date = date;
        return this;
    }

    public Date Build()
        => new Date(this._date);
}