namespace Entities.Exceptions
{
    public sealed class EmployeeInvalidModelException : InvalidModelException
    {
        public EmployeeInvalidModelException(string message) 
            :base($"{message}")
        {
        }
    }
}
