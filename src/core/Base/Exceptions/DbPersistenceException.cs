namespace Base.Exceptions
{
    public class DbPersistenceException : ApplicationException
    {
        public string MessageLogger { get; }

        public DbPersistenceException(string message, string messageLogger)
            : base(message)
        {
            // Concatenar el mensaje base con la info adicional
            MessageLogger = $"{message} - {messageLogger}";
        }
    }
}