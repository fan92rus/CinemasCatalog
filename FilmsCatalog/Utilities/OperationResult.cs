namespace FilmsCatalog.Services
{
    public class OperationResult
    {
        public OperationResult(bool succeeded, string message = "")
        {
            Succeeded = succeeded;
            Message = message;
        }

        public bool Succeeded { get; }
        public string Message { get; }
    }
    public class OperationResult<T> : OperationResult
    {
        public OperationResult(bool succeeded, T value, string message) : base(succeeded, message)
        {
            this.Value = value;
        }
        public T Value { get; set; }
    }
}