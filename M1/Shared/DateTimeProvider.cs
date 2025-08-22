namespace M1.Shared
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Agora => DateTime.Now;
        public DateTime AgoraUniversal => DateTime.UtcNow;
    }
}
