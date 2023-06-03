namespace RTSADocs.Shared.DTOs
{
    public class Result<T> : Result
    {
        public T? Data { get; init; }

        private Result(bool succeeded, T? data, IEnumerable<string>? messages = default) : base(succeeded, messages)
        {
            Data = data;
            Succeeded = succeeded;
            if (messages != null && messages.Any())
            {
                Messages = messages.ToList();
            }
            else
            {
                Messages = new List<string>();
            }
        }
        public static Result<T> Success(T data, IEnumerable<string> messages)
        {
            return new Result<T>(true, data, messages);
        }
        public static Result<T> Success(T data)
        {
            return new Result<T>(true, data);
        }
        public new static Result<T> Failure(string message)
        {
            return new Result<T>(false,default, new List<string> { message });
        }

    }
    public class Result
    {
        public List<string> Messages { get; init; }
        public bool Succeeded { get; init; }
        protected Result(bool succeeded, IEnumerable<string>? messages = default)
        {
            Succeeded = succeeded;
            if (messages != null && messages.Any())
            {
                Messages = messages.ToList();
            }
            else
            {
                Messages = new List<string>();
            }
        }
        public static Result Success(IEnumerable<string> messages)
        {
            return new Result(true, messages);
        }
        public static Result Success()
        {
            return new Result(true);
        }
        public static Result Failure()
        {
            return new Result(false, default);
        }
        public static Result Failure(IEnumerable<string> messages)
        {
            return new Result(false, messages);
        }  
        public static Result Failure(string message)
        {
            return new Result(false, new List<string> { message });
        }
    }
}
