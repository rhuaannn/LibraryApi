using AutoMapper;
using Library_Application.Interfaces;
using Library_Application.Services;
using Library_Application.BookDTO;
using Microsoft.AspNetCore.Mvc;
using Library_Domain.Model;

namespace Library_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBook _bookService;
        public BookController( IBook bookServices)
        {
            _bookService = bookServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var booksDTO = await _bookService.GetAllBooks();
            if (booksDTO == null || booksDTO.Count == 0)
                return NotFound("No books found");

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
    }

}
