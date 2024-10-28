using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; } 
        public string? Message { get; set; }   
        public string? Error { get; set; }     

        public ApiResponse(T data, string? message = null)
        {
            Success = true;
            Data = data;
            Message = message;
            Error = null;
        }

        public ApiResponse(string error)
        {
            Success = false;
            Error = error;
            Data = default; 
            Message = null;
        }
    }
}
