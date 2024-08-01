namespace Library.Api.Domain.Abstracts
{
    public class Result<TValue> : Result
    {
        public readonly TValue? Value;

        protected Result(TValue value) : base()
        {
            Value = value;
        }

        protected Result(Exception error) : base(error)
        {
            Value = default;
        }

        public static Result<TValue> Ok(TValue value)
            => new Result<TValue>(value);

        public static Result<TValue> Fail(Exception error)
            => new Result<TValue>(error);
    }

    public class Result
    {
        public readonly Exception? Error;
        public readonly bool Success;

        protected Result()
        {
            Success = true;
            Error = default;
        }

        protected Result(Exception error)
        {
            Success = false;
            Error = error;
        }

        public static Result Ok() => new Result();
        public static Result Fail(Exception error) => new Result(error);
        public static Result Fail(string message)
            => new Result(new Exception(message));
    }
}
