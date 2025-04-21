using AutoMapper;
using Library_Application.BookDTO;
using Library_Application.Interfaces;
using Library_Domain.Model;
using Library_Infra.Connect;
using Microsoft.EntityFrameworkCore;
namespace Library_Application.Services
{
    public class BookServices : IBook
    {
        private readonly DBConnection _dbconnection;
        private readonly IMapper _mapper;

        public BookServices(DBConnection dbConnection, IMapper mapper)
        {
            _dbconnection = dbConnection;
            _mapper = mapper;
        }

        public async Task<RequestCreateBookDTO> AddBook(RequestCreateBookDTO bookDTO)
        {
            var createBook = _mapper.Map<Book>(bookDTO);
            var addedBook = await _dbconnection.Books.AddAsync(createBook); 
            if (addedBook == null)
            {
                throw new Exception("Error creating book");
            }
            await _dbconnection.SaveChangesAsync();
            return _mapper.Map<RequestCreateBookDTO>(createBook); 
        }

        public async Task<RequestDeleteDTO> DeleteBook(Guid id)
        {
            var book = await _dbconnection.Books.FindAsync(id); // Corrected to use FindAsync to locate the book by ID
            if (book == null)
            {
                throw new Exception("Book not found");
            }
            _dbconnection.Books.Remove(book); // Corrected to use Remove instead of RemoveAsync
            await _dbconnection.SaveChangesAsync(); // Ensure changes are saved to the database
            return new RequestDeleteDTO { Id = id }; // Return a meaningful response
        }

        public async Task<List<ResponseBookDTO>> GetAllBooks()
        {
            var books = await _dbconnection.Books.ToListAsync();
            return _mapper.Map<List<ResponseBookDTO>>(books);
        }

        public Task<Book> GetBookById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetBooksByAuthor(string authorName)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetBooksByGenre(string genreName)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseBookDTO> UpdateBook(Guid id, RequestUpdateDTO requestUpdateDTO)
        {
            var book = await _dbconnection.Books.FindAsync(id);
            if (book == null)
            {
                throw new Exception("Book not found");
            }
            if(book.Id != id)
            {
                throw new Exception("Book not found");
            }

            _mapper.Map(requestUpdateDTO, book);
            _dbconnection.Books.Update(book);
            await _dbconnection.SaveChangesAsync();

            return _mapper.Map<ResponseBookDTO>(book);
        }
    }
}