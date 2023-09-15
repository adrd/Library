namespace Library.ConsoleApp.Model
{
    public sealed class AddBookInputModel
    {
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public string Isbn { get; set; }
        public string RentalPrice { get; set; }
    }
}
