namespace School.API.Common
{
    public class EntityInvalidException : Exception
    {
        public EntityInvalidException(string exceptionName, string message = null) : base(message) {
            ExceptionName = exceptionName; 
        }

        public string ExceptionName { get; }
    }
}
