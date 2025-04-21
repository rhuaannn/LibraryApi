using Library_Application.BookDTO;
using Library_Domain.Model;

namespace Library_Application.Interfaces
{
    public interface IBook
    {
        public Task<List<ResponseBookDTO>> GetAllBooks();

        public Task<Book> GetBookById(Guid id);

        public Task<RequestCreateBookDTO> AddBook(RequestCreateBookDTO bookDTO);

        public Task<ResponseBookDTO> UpdateBook(Guid id, RequestUpdateDTO requestUpdateDTO);

        public Task<RequestDeleteDTO> DeleteBook(Guid id);

        public Task<List<Book>> GetBooksByAuthor(string authorName);

        public Task<List<Book>> GetBooksByGenre(string genreName);
    }
}
