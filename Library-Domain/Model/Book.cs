using Library_Domain.ValueObject;

namespace Library_Domain.Model
{
    public class Book
    {
        private Title title;
        private Author author;
        private Genre genre;
       // private PublishedDate publishedDate;

        public Guid Id { get; private set; } = Guid.NewGuid();
        public Title Title { get;  set; }
        public Author Author { get;  set; }
        public Genre Genre { get;  set; }
        public DateTime PublishedDate { get;  set; } = DateTime.Now;
        public string Description { get; set; } = string.Empty;
        protected Book()
        {
            
        }

        public Book(Title title, Author author, Genre genre, DateTime publishedDate, string description)
        {
            Title = title;
            Author = author;
            Genre = genre;
            Description = description;
            PublishedDate = publishedDate;

        }

    }
}
