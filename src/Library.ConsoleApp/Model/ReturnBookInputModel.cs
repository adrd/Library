namespace Library.ConsoleApp.Model;

public sealed class ReturnBookInputModel
{
    public string BookTitle { get; set; }
    public string BookAuthor { get; set; }
    public string Isbn { get; set; }

    public string ReaderName { get; set; }

    public string CardNumber { get; set; }

    public string ReturnDate { get; set; }
}