using Library_Domain.ValueObject;

namespace Library_Domain.Model
{
    public class Book
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Title Title { get; private set; }
        public Author Author { get; private set; }
        public Genre Genre { get; private set; }
        public PublishedDate PublishedDate { get; private set; }
        protected Book()
        {
            
        }

        public Book(Title title, Author author, Genre genre, PublishedDate publishedDate)
        {
            Title = title;
            Author = author;
            Genre = genre;
            PublishedDate = publishedDate;

        }
    }
}
