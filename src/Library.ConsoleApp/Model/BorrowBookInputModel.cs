namespace Library.ConsoleApp.Model;

public sealed class BorrowBookInputModel
{
    public string BookTitle { get; set; }
    public string BookAuthor { get; set; }
    public string Isbn { get; set; }

    public string ReaderName { get; set; }

    public string CardNumber { get; set; }
}