namespace M1.Shared
{
    public interface IDateTimeProvider
    {
        public DateTime Agora { get; }
        public DateTime AgoraUniversal { get; }
    }
}
