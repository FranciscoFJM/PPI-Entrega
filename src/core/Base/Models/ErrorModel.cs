namespace Base.Models
{
    public class ErrorModel
    {
        public string Message { get; set; }
        public ErrorTypeEnum ErrorCode { get; set; }

        public enum ErrorTypeEnum
        {
            InputDataError,
            DatabaseError,
            InternalError
        }
    }
}