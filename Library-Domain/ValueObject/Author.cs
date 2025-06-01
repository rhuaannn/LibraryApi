namespace Library_Domain.ValueObject
{
    public class Author
    {
        public string Value { get; }

        protected Author()
        {
        }
        public Author(string value)
        {
            if(string.IsNullOrEmpty(value))
               throw new ArgumentNullException("Autor não pode ser nulo");
            Value = value;
        }

        public override string ToString() => Value;
    }
}
