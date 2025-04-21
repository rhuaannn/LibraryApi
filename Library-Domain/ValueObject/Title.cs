namespace Library_Domain.ValueObject
{
    public class Title
    {
        public string Value { get; } = string.Empty;

        protected Title()
        {
            
        }
        public Title(string title)
        {
            if(IsValid() || title.Length > 3)
            {
                Value = title;
            }
            else
            {
                throw new ArgumentException("Title cannot be null or empty.");
            }           
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Value);
        }
        public override string ToString()
        {
            return Value;
        }
        public static implicit operator string(Title title)
        {
            return title.Value;
        }
    }
}
