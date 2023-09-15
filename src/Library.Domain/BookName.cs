using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain;

public sealed record BookName
{
    private readonly string _title;
    private readonly string _authorName;

    public BookName(string title, string authorName)
    {
        this._title = title;
        this._authorName = authorName;
    }

    public override string ToString()
    {
        return $"{this._title.ToUpper()}, {this._authorName.ToUpper()}";
    }
}