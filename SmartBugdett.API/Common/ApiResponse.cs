namespace SmartBudgett.API.Common
{

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; } = new();

        public ApiResponse()
        { }

        public ApiResponse(bool success, string message, T data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public ApiResponse(bool success, string message, T data, List<string> errors) : this(success, message, data)
        {
            Errors = errors;
        }

       
        public static ApiResponse<T> Ok(T data, string message = "Operation successful")
        {
            return new ApiResponse<T>(true, message, data);
        }

        public static ApiResponse<T> Ok(string message = "Operation successful")
        {
            return new ApiResponse<T>(true, message);
        }

        public static ApiResponse<T> Error(string message, List<string> errors = null)
        {
            return new ApiResponse<T>(false, message, default, errors ?? new List<string>());
        }

        public static ApiResponse<T> Error(string message, string error)
        {
            return new ApiResponse<T>(false, message, default, new List<string> { error });
        }
    }


    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new();

        public ApiResponse()
        { }

        public ApiResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public ApiResponse(bool success, string message, List<string> errors) : this(success, message)
        {
            Errors = errors;
        }

    
        public static ApiResponse Ok(string message = "Operation successful")
        {
            return new ApiResponse(true, message);
        }

        public static ApiResponse Error(string message, List<string> errors = null)
        {
            return new ApiResponse(false, message, errors ?? new List<string>());
        }

        public static ApiResponse Error(string message, string error)
        {
            return new ApiResponse(false, message, new List<string> { error });
        }
    }
}
