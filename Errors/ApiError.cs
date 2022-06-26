using System.Text.Json;

namespace BuyandRentHomeWebAPI.Errors
{
    public class ApiError
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }

        public ApiError(int errorCode, string errorMessage, string errorDetails = null)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            ErrorDetails = errorDetails;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
