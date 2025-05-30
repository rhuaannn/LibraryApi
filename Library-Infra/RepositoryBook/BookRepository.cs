using Library_Domain.Model;
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

        public async Task<List<Book>> GetAllBooks(int skip, int take, CancellationToken cancellationToken = default)
        {
            return await _dbConnection.Books
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);
        }
    }
}
