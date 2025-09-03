namespace Base.Models
{
    public class ResultModel<T>
    {
        public T Data { get; set; }
        public bool Ok { get { return !Errors.Any(); } }
        public string Message { get; set; }
        public List<ErrorModel> Errors { get; set; }

        public ResultModel(T obj) 
        {
            Data = obj;
        }

        public ResultModel() 
        {
            Errors = new List<ErrorModel>();
        }

        private void AddError(string message, ErrorModel.ErrorTypeEnum errorCode)
        {
            Errors.Add(new ErrorModel { Message = message, ErrorCode = errorCode });
        }

        public void AddDataBaseError(string message) // Usar para los errores de la capa de persistencia
        {
            AddError(message, ErrorModel.ErrorTypeEnum.DatabaseError);
        }

        public void AddInputDataError(string message) // Usar para los errores de validación / formato de datos
        {
            AddError(message, ErrorModel.ErrorTypeEnum.InputDataError);
        }

        public void AddInternalError(string message) // Usar para los errores internos
        {
            AddError(message, ErrorModel.ErrorTypeEnum.InternalError);
        }
    }
}