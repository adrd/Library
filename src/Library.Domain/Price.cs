using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain;

public sealed record Price
{
    private readonly Money _amount;

    public Price(Money amount)
    {
        this._amount = amount;
    }

    public static Decimal operator *(Price price, Int32 value)
    {
        Decimal result = price._amount * value;

        return result;
    }

    public override string ToString()
    {
        return this._amount.ToString();
    }
}