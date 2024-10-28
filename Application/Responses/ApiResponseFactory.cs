using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public static class ApiResponseFactory
    {
        public static ApiResponse<T> Success<T>(T data, string message = "")
        {
            return new ApiResponse<T>(data, message);
        }

        public static ApiResponse<T> Error<T>(string error)
        {
            return new ApiResponse<T>(error);
        }
    }

}
