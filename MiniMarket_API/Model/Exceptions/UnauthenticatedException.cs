namespace MiniMarket_API.Model.Exceptions
{
    public class UnauthenticatedException : Exception
    {
        public UnauthenticatedException(string message) : base(message) { }
    }
}
