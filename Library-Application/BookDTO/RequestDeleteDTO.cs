using System.ComponentModel.DataAnnotations;

namespace Library_Application.BookDTO
{
    public class RequestDeleteDTO
    {
      public string Message { get; internal set; }
      public Guid Id { get; internal set; }
        public RequestDeleteDTO()
        {
            
        }
        public RequestDeleteDTO(string message)
        {
            Message = message;
        }
    }
}
