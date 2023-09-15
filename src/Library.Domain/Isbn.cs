using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain;

public sealed record Isbn
{
    private readonly string _isbn;

    public Isbn(string isbn)
    {
        this._isbn = isbn;
    }

    public override string ToString()
    {
        return $"{this._isbn}";
    }
}