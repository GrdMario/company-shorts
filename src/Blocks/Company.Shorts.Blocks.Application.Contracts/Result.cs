namespace Company.Shorts.Blocks.Application.Contracts
{
    public abstract class Result
    {
        public bool Success { get; protected set; }

        public bool Failure => !Success;
    }

    public abstract class Result<T> : Result
    {
        private readonly T _data;

        protected Result(T data)
        {
            _data = data;
        }

        public T Data
        {
            get => Success
                ? _data
                : throw new Exception("You can't access the data of failed result.");
        }
    }

    public class SuccessResult : Result
    {
        public SuccessResult()
        {
            Success = true;
        }
    }

    public class SuccessResult<T> : Result<T>
    {
        public SuccessResult(T data) : base(data)
        {
            Success = true;
        }
    }

    public class ErrorResult : Result
    {
        public ErrorResult(string message)
            : this(message, Array.Empty<Error>())
        {
        }

        public ErrorResult(
            string message,
            IReadOnlyCollection<Error> errors)
        {
            Success = false;
            Message = message;
            Errors = errors;
        }

        public string Message { get; }

        public IReadOnlyCollection<Error> Errors { get; }
    }

    public class ErrorResult<T> : Result<T>
    {
        public ErrorResult(string message)
            : this(message, Array.Empty<Error>())
        {
        }

        public ErrorResult(
            string message,
            IReadOnlyCollection<Error> errors)
            : base(default!)
        {
            Success = false;
            Message = message;
            Errors = errors;
        }

        public string Message { get; }

        public IReadOnlyCollection<Error> Errors { get; }
    }
    public class Error
    {
        public Error(
            string details)
            : this(null, details)
        {
        }

        public Error(
            string? code,
            string details)
        {
            Code = code;
            Details = details;
        }

        public string? Code { get; }

        public string Details { get; }
    }
}