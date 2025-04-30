using Library_Application.BookDTO;
using Library_Application.Interfaces;
using Library_Domain.Model;
using Library_Infra.Redis;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization; 

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
        public async Task<IActionResult> GetAllBooks()
        {
            var bookCache = await _cache.GetAsync<List<ResponseBookDTO>>("books");
            if (bookCache != null)
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
        public async Task<IActionResult> AddBook([FromBody] RequestCreateBookDTO bookDTO)
        {
            if (bookDTO == null)
                return BadRequest("Invalid book data");
            var book = await _bookService.AddBook(bookDTO);
            if (book == null)
                return BadRequest("Error creating book");
            return CreatedAtAction(nameof(GetAllBooks), new { id = bookDTO.Author }, book);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] RequestUpdateDTO dto)
        {
            var updatedBook = await _bookService.UpdateBook(id, dto);
            return Ok(updatedBook);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedBook = await _bookService.DeleteBook(id);
            if (deletedBook == null)
                return NotFound("Book not found");
            return Ok(deletedBook);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var bookCaching = await _cache.GetAsync<ResponseBookDTO>(id.ToString());
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

        public async Task<IActionResult> GetByAuthor(string author)
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
