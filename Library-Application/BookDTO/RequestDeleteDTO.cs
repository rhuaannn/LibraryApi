namespace Library_Application.BookDTO
{
    public class RequestDeleteDTO
    {
        public Guid Id { get; set; }

        public RequestDeleteDTO()
        {
            
        }
        public RequestDeleteDTO(Guid id)
        {
            Id = id;
        }
    }
}
