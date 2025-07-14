namespace IAMNC0000S03.Business.Models
{
    public class ApiResponse
    {
        public class ApiResponsewithData<T>
        {
            public string Result { get; set; }
            public string Message { get; set; }
            public List<T> Data { get; set; } = [];

            public ApiResponsewithData()
            {
                Result = "0";
                Message = "success";
            }

            public ApiResponsewithData(string message)
            {
                Result = "1";
                Message = message;
            }
        }

        public class ApiResponseWithFlag
        {
            public string Result { get; set; } = "0";
            public string Message { get; set; } = string.Empty;
            public bool IsExist { get; set; }
        }
    }
}