namespace Library_Domain.ValueObject
{
    public class Author
    {
        public string Value { get; }

        public Author(string value)
        {
            Value = value;
        }

        public override string ToString() => Value;
    }
}
