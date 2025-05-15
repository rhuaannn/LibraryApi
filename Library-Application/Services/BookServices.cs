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
            var book = await _dbconnection.Books.FindAsync(id);  
            if (book == null)
            {
                throw new Exception("Book not found");
            }
            _dbconnection.Books.Remove(book);  
            await _dbconnection.SaveChangesAsync();
            return new RequestDeleteDTO { Message = "Book deleted successfully" }; 
        }

        public async Task<List<ResponseBookDTO>> GetAllBooks()
        {
            var books = await _dbconnection.Books.ToListAsync();
            return _mapper.Map<List<ResponseBookDTO>>(books);
        }

        public async Task<ResponseBookDTO> GetBookById(Guid id)
        {
            var book = await _dbconnection.Books.FindAsync(id);

             if (book == null)
            {
                throw new Exception("Book not found");
            }
            return _mapper.Map<ResponseBookDTO>(book);
        }

        public async Task<List<Book>> GetBooksByAuthor(string authorName)
        {
            var books = await _dbconnection.Books 
                .Where(b => b.Author.Value.Contains(authorName))
                .ToListAsync();
            if (books == null || books.Count == 0)
            {
                throw new Exception("No books found for this author");
            }
                return books;
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