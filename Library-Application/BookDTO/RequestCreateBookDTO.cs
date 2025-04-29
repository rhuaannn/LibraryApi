using System.ComponentModel.DataAnnotations;
namespace Library_Application.BookDTO
{
    public class RequestCreateBookDTO
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50, ErrorMessage = "Title cannot exceed 50 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required")]
        [MinLength(2, ErrorMessage = "Author cannot min 2 characters")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Published date is required")]
        public DateTime PublishedDate { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        public RequestCreateBookDTO()
        {
        }

        public RequestCreateBookDTO(string title, string author, string genre, DateTime publishedDate, string description)
        {
            Title = title;
            Author = author;
            Genre = genre;
            PublishedDate = publishedDate;
            Description = description;
        }
    }
}
