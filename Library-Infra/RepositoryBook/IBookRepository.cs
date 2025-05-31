using Library_Domain.Model;

namespace Library_Infra.RepositoryBook
{
    public interface IBookRepository
    {
        public Task<List<Book?>> GetAllBooks(int skip, int take, CancellationToken cancellationToken = default);
        public Task<Book> GetBookById(Guid id);
        public Task<Book> AddBook(Book book);
        public Task<Book> UpdateBook(Guid id);
        public Task DeleteBook(Guid id);
        public Task<List<Book>> GetBooksByAuthor(string authorName);
        public Task<List<Book>> GetBooksByGenre(string genreName);
    }
}
