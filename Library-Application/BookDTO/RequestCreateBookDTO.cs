namespace Library_Application.BookDTO
{
   public class RequestCreateBookDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public DateTime PublishedDate { get; set; }

        public string Description { get; set; } = string.Empty;

        public RequestCreateBookDTO()
        {
            
        }
        public RequestCreateBookDTO(string title, string author, string genre, DateTime publishedDate, string description)
        {
            Title = title;
            Author = author;
            Genre = genre;
            PublishedDate = publishedDate;
            Description = description;
        }

    }
}
