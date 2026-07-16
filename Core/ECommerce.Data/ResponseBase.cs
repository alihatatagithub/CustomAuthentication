using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data
{
    public abstract class ResponseBase
    {
        public ResponseBase()
        {
            IsValid = true;
            Errors = new List<string>();
        }

        public List<string> Errors { get; set; }
        public bool IsValid { get; set; }
    }
    public class Response<T> : ResponseBase //where T : class
    {
        public Response() : base() { }

        public T Model { get; set; }
    }
    public class ResponseList<T> : ResponseBase //where T : class
    {
        public ResponseList() : base() { }

        public List<T> Model { get; set; }
        public long Count { get; set; }
    }
    public class SuccessDTO
    {
        public bool IsSuccess { get; set; }
    }
}
