namespace Library_Domain.ValueObject
{
    public class Genre
    {
        public string Value { get; }

        protected Genre()
        {
            
        }
        public Genre(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Gênero não pode ser vazio ou nulo.");

            Value = value;
        }

        public override string ToString() => Value;
    }
}
