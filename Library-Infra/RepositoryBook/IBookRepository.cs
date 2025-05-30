using Library_Domain.Model;

namespace Library_Infra.RepositoryBook
{
    public interface IBookRepository
    {
        public Task<List<Book?>> GetAllBooks(int skip, int take, CancellationToken cancellationToken = default);

    }
}
