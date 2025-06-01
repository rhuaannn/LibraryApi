using AutoMapper;
using Library_Application.BookDTO;
using Library_Application.Interfaces;
using Library_Domain.Model;
using Library_Infra.Connect;
using Library_Infra.RepositoryBook;
using Microsoft.EntityFrameworkCore;
namespace Library_Application.Services
{
    public class BookServices: IBook
    {
        private readonly DBConnection _dbconnection;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookServices(IBookRepository bookRepository, IMapper mapper, DBConnection dbConnection)
        {
            _dbconnection = dbConnection;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<RequestCreateBookDTO> AddBook(RequestCreateBookDTO bookDTO)
        {

            var createBook = _mapper.Map<Book>(bookDTO);
            var addedBook = await _bookRepository.AddBook(createBook);
            if (addedBook == null)
            {
                throw new Exception("Error creating book");
            }

            return _mapper.Map<RequestCreateBookDTO>(addedBook);
        }

        public async Task DeleteBook(Guid id)
        {

            var book = await _bookRepository.GetBookById(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} not found.");
            }
             await _bookRepository.DeleteBook(id);
            
        }

        public async Task<List<ResponseBookDTO>> GetAllBooks(int skip, int take)
        {
            var books = await _bookRepository.GetAllBooks(skip, take);
            return _mapper.Map<List<ResponseBookDTO>>(books);
        }

        public async Task<ResponseBookDTO> GetBookById(Guid id)
        {
            var book = await _dbconnection.Books.FindAsync(id);

            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} not found.");
            }
            return _mapper.Map<ResponseBookDTO>(book);
        }

        public Task<List<Book>> GetBooksByGenre(string genreName)
        {
            throw new NotImplementedException();
        }

        public async Task<RequestUpdateDTO> UpdateBook(Guid id, RequestUpdateDTO requestUpdateDTO)
        {
            var book = await _bookRepository.GetBookById(id);
            if (book == null)
            {
                throw new Exception("Book not found");
            }
            if(book.Id != id)
            {
                throw new Exception("Book not found");
            }

            _mapper.Map(requestUpdateDTO, book);
            var updatedBook = await _bookRepository.UpdateBook(book);
            return _mapper.Map<RequestUpdateDTO>(book);
        }


        public async Task<List<ResponseBookDTO>> GetBooksByAuthor(string authorName)
        {
            var books = await _bookRepository.GetBooksByAuthor(authorName);
            return _mapper.Map<List<ResponseBookDTO>>(books);
        }
    }
}