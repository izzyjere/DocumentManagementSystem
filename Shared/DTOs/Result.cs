using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTSADocs.Shared.DTOs
{
    public class Result<T> : Result
    {
        public T Data { get; init; }
        public List<string> Messages { get; init; }
        public bool Succeeded { get; init; }
        private Result(bool succeeded, T data, IEnumerable<string>? messages = default) : base(succeeded, messages)
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
    }
}
