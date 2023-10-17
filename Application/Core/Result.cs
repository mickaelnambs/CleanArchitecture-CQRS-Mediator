namespace Application.Core
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string Error { get; set; }

        public static Result<T> Success(T Value) => new() { IsSuccess = true, Value = Value };
        public static Result<T> Failure(string error) => new() { IsSuccess = false, Error = error };
    }
}