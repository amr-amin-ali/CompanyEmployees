namespace Entities.Exceptions
{
    public abstract class InvalidModelException : Exception
    {
        protected InvalidModelException(string message) : base(message)
        {
        }
    }
}
