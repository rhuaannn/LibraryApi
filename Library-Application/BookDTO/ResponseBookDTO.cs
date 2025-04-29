using System.ComponentModel.DataAnnotations;

namespace Library_Application.BookDTO
{
    public class ResponseBookDTO
    {
        public Guid Id { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Genre { get; set; } = string.Empty;

        public ResponseBookDTO()
        {
            
        }

    }
}
