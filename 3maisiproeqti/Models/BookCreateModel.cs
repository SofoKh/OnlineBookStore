namespace _3maisiproeqti.Models
{
    public class BookCreateModel
    {

        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int PublicationYear { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }
}
