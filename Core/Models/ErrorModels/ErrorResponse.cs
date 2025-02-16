using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.ErrorModels
{
    public class ErrorResponse
    {
        public int StatusCode {  get; set; }
        public string? Message { get; set; }
        public string? Detail { get; set; }

        public ErrorResponse(int statusCode, string? message, string? detail)
        {
            StatusCode = statusCode;
            Message = message;
            Detail = detail;
        }
    }
}
