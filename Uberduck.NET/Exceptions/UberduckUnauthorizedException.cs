namespace Uberduck.NET.Exceptions
{
    public class UberduckUnauthorizedException : Exception
    {
        public UberduckUnauthorizedException() { }

        public UberduckUnauthorizedException(string message) : base(message) { }

        public UberduckUnauthorizedException(string message, HttpRequestException innerException) : base(message, innerException) { }
    }
}
