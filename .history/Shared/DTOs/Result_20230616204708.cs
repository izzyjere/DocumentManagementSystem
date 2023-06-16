namespace RTSADocs.Shared.DTOs
{
    public class Result<T> : Result
    {
        // This generic class represents a result with data of type T.
        // It inherits from the non-generic Result class.

        public T? Data { get; init; }

        private Result(bool succeeded, T? data, IEnumerable<string>? messages = default) : base(succeeded, messages)
        {
            Data = data;
            Succeeded = succeeded;

            // Check if messages are provided and initialize the Messages property accordingly
            if (messages != null && messages.Any())
            {
                Messages = messages.ToList();
            }
            else
            {
                Messages = new List<string>();
            }
        }

        // Creates a success result with data and messages
        public static Result<T> Success(T data, IEnumerable<string> messages)
        {
            return new Result<T>(true, data, messages);
        }

        // Creates a success result with data
        public static Result<T> Success(T data)
        {
            return new Result<T>(true, data);
        }

        // Creates a failure result with a single error message
        public new static Result<T> Failure(string message)
        {
            return new Result<T>(false, default, new List<string> { message });
        }
    }

    public class Result
    {
        // This class represents a result without data.
        // It is the base class for the generic Result<T> class.

        public List<string> Messages { get; init; }
        public bool Succeeded { get; init; }

        protected Result(bool succeeded, IEnumerable<string>? messages = default)
        {
            Succeeded = succeeded;

            // Check if messages are provided and initialize the Messages property accordingly
            if (messages != null && messages.Any())
            {
                Messages = messages.ToList();
            }
            else
            {
                Messages = new List<string>();
            }
        }

        // Creates a success result with messages
        public static Result Success(IEnumerable<string> messages)
        {
            return new Result(true, messages);
        }

        // Creates a success result without messages
        public static Result Success()
        {
            return new Result(true);
        }

        // Creates a failure result without messages
        public static Result Failure()
        {
            return new Result(false, default);
        }

        // Creates a failure result with messages
        public static Result Failure(IEnumerable<string> messages)
        {
            return new Result(false, messages);
        }

        // Creates a failure result with a single error message
        public static Result Failure(string message)
        {
            return new Result(false, new List<string> { message });
        }

        // Creates a success result with a single message
        public static Result Success(string message)
        {
            return new Result(true, new List<string> { message });
        }
    }
}
