namespace ORAA.Core
{
    public class ApiResponseFactory
    {
        public static ApiResponse<T> CreateResponse<T>(int status, string message, T data)
        {
            return new ApiResponse<T>
            {
                Status = status,
                Message = message,
                Data = data
            };
        }
    }
}
