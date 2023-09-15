namespace Library.Domain;

public record Date
{
    private readonly DateTime _date;

    public Date(DateTime date)
    {
        this._date = date;
    }

    public Date Add(int days)
    {
        DateTime dateTime = this._date.AddDays(days); 
        return new Date(dateTime);
    }

    public TimeSpan Substract(Date other)
    {
        return this._date.Subtract(other._date);
    }

    public static bool operator <(Date left, Date right)
        => left._date < right._date;

    public static bool operator >(Date left, Date right)
        => left._date > right._date;

    public static bool operator <=(Date left, Date right)
        => left._date <= right._date;

    public static bool operator >=(Date left, Date right)
        => left._date >= right._date;

    public override string ToString()
    {
        return $"{this._date.Day}.{this._date.Month}.{this._date.Year}";
    }
}