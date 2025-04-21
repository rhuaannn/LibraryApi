namespace Library_Application.BookDTO
{
    public class RequestUpdateDTO
    {

       
        public string Title { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public string Description { get; set; } = string.Empty;

        public RequestUpdateDTO() { }

        public RequestUpdateDTO(string title, string author, string genre, DateTime publishedDate)
        {
            Title = title;
            Author = author;
            Genre = genre;
            PublishedDate = publishedDate;
        }
    }
}
