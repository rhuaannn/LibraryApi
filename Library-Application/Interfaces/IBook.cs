using Library_Application.BookDTO;
using Library_Domain.Model;

namespace Library_Application.Interfaces
{
    public interface IBook
    {
        Task<ResponseBookDTO> GetBookById(Guid id);
        Task<RequestCreateBookDTO> AddBook(RequestCreateBookDTO bookDTO);
        Task<ResponseBookDTO> UpdateBook(Guid id, RequestUpdateDTO requestUpdateDTO);
        Task<RequestDeleteDTO> DeleteBook(Guid id);
        Task<List<Book>> GetBooksByAuthor(string authorName);
        Task<List<Book>> GetBooksByGenre(string genreName);
        Task<List<ResponseBookDTO>> GetAllBooks(int skip, int take); 
    }
}
