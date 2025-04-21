namespace Library_Domain.ValueObject
{
   public class PublishedDate
    {
        public DateTime Date { get; }

        protected PublishedDate()
        {
            
        }
        public PublishedDate(DateTime date)
        {
            if(!IsValid())
            {
                Date = date;
            }
            else
            {
                throw new ArgumentException("Published date cannot be null or empty.");
            }
        }

        public bool IsValid()
        {
            var min = new DateTime(1900, 1, 1);
            return Date >= min && Date <= DateTime.Now;
        }

        public override string ToString()
        {
            return Date.ToString("yyyy-MM-dd");
        }
    }
}
