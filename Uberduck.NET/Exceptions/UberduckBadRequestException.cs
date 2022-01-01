namespace Uberduck.NET.Exceptions
{
    public class UberduckBadRequestException : Exception
    {
        public UberduckBadRequestException() { }

        public UberduckBadRequestException(string message) : base(message) { }

        public UberduckBadRequestException (string message, HttpRequestException innerException) : base(message, innerException) { }
    }
}
