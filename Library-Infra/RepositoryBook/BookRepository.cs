using Library_Domain.Model;
using Library_Domain.ValueObject;
using Library_Infra.Connect;
using Microsoft.EntityFrameworkCore;

namespace Library_Infra.RepositoryBook
{
    public class BookRepository : IBookRepository
    {
        private readonly DBConnection _dbConnection;

        public BookRepository(DBConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Book> AddBook(Book book)
        {
           var addedBook = await _dbConnection.Books.AddAsync(book);
            await _dbConnection.SaveChangesAsync();
            return addedBook.Entity;
        }

        public async Task DeleteBook(Guid id)
        {
            var deleteBook = await _dbConnection.Books.FirstOrDefaultAsync(b => b.Id == id);
            await _dbConnection.Books.ExecuteDeleteAsync();
            await _dbConnection.SaveChangesAsync();

        }

        public async Task<List<Book>> GetAllBooks(int skip, int take, CancellationToken cancellationToken = default)
        {
            return await _dbConnection.Books
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);
        }

        public async Task<Book> GetBookById(Guid id)
        {
            var book = await _dbConnection.Books.FindAsync(id);
            return book;
        }

        public async Task<List<Book>> GetBooksByAuthor(string authorName)
        {

            var books = await _dbConnection.Books
                .Where(b => b.Author.Value.Contains(authorName))
                .ToListAsync();
            return books;
        }

        public Task<List<Book>> GetBooksByGenre(string genreName)
        {
            throw new NotImplementedException();
        }

        public async Task<Book> UpdateBook(Book book)
        {
            _dbConnection.Books.Update(book);
            await _dbConnection.SaveChangesAsync();
            return book;
        }
    }
}
