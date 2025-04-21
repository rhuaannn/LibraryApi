namespace Library_Domain.ValueObject
{
   public class Author
    {
        public string Name { get; } = string.Empty;

        protected Author()
        {
            
        }

        public Author(string name)
        {
            
            if (IsValid() || name.Length > 3)
            {
                Name = name;
            }
            else
            {
                throw new ArgumentException("Author cannot be null or empty.");
            }
        }
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }
        public override string ToString()
        {
            return Name;
        }
        public static implicit operator string(Author author)
        {
            return author.Name;
        }
    }
}
