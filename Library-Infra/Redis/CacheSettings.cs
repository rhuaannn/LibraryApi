namespace Library_Infra.Redis
{
    public class CacheSettings
    {
        public int AbsoluteExpirationRelativeToNow {get;set;}
        public int SlidingExpiration {get;set; }
    }
}
