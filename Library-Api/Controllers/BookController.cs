using Library_Application.BookDTO;
using Library_Application.Interfaces;
using Library_Infra.Redis;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Library_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class BookController : ControllerBase
    {
        private readonly IBook _bookService;
        private readonly ICachingService _cache;
        public BookController( IBook bookServices, ICachingService cache)
        {
            _bookService = bookServices;
            _cache = cache;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var bookCache = await _cache.GetAsync("Books"); // Sem tipo genérico aqui
            if (!string.IsNullOrEmpty(bookCache))
            {
                var books = JsonSerializer.Deserialize<List<ResponseBookDTO>>(bookCache);
                return Ok(books);
            }

            var booksDTO = await _bookService.GetAllBooks();
            if (booksDTO == null || booksDTO.Count == 0)
                return NotFound("No books found");

            await _cache.SetAsync("books", JsonSerializer.Serialize(booksDTO));
            return Ok(booksDTO);
        }


        [HttpPost]
        public async Task<IActionResult> AddBookAsync([FromBody] RequestCreateBookDTO bookDTO)
        {
            if (bookDTO == null)
                return BadRequest("Invalid book data");
            var book = await _bookService.AddBook(bookDTO);
            if (book == null)
                return BadRequest("Error creating book");
            await _cache.RemoveAsyc("books");
            return CreatedAtAction(nameof(GetAllBooksAsync), new { id = bookDTO.Author }, book);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync(Guid id, [FromBody] RequestUpdateDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid update data");

            var bookToUpdate = await _bookService.GetBookById(id);
            if (bookToUpdate == null)
                return NotFound("Book not found");

            var updatedBook = await _bookService.UpdateBook(id, dto);
            if (updatedBook == null)
                return BadRequest("Error updating book");

            await _cache.SetAsync(id.ToString(), JsonSerializer.Serialize(updatedBook));
            await _cache.RemoveAsyc("books");
            return Ok(updatedBook);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync(Guid id)
        {
            await _cache.RemoveAsyc(id.ToString());
            var deletedBook = await _bookService.DeleteBook(id);
            if (deletedBook == null)
                return NotFound("Book not found");
            await _cache.RemoveAsyc("books");
            return Ok(deletedBook);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookByIdAsync(Guid id)
        {
            var bookCaching = await _cache.GetAsync(id.ToString());
            if (!string.IsNullOrEmpty(bookCaching))
            {
                var book = JsonSerializer.Deserialize<ResponseBookDTO>(bookCaching);
                return Ok(book);
            }
            var bookById = await _bookService.GetBookById(id);
            if (bookById == null)
                return NotFound("Book not found");
            return Ok(bookById);
        }

        [HttpGet("author")]

        public async Task<IActionResult> GetByAuthorAsync(string author)
        {
            var BookAuthor = await _bookService.GetBooksByAuthor(author);
            if (BookAuthor == null)
            {
                return NotFound("Book not found");
            }
            return Ok(BookAuthor);

        }

    }

}
