using Electric.API.Constants;
using Electric.API.Dtos.Common;

namespace Electric.API.Helpers
{
    public class ResponseHelper
    {
        public static ResponseDto<T> Created<T>(
            string message,
            T data
        )
        {
            return new ResponseDto<T>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = message,
                Data = data
            };
        }

        public static ResponseDto<T> OK<T>(
            string message,
            T data
        )
        {
            return new ResponseDto<T>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = message,
                Data = data
            };
        }

        public static ResponseDto<T> BadRequest<T>(
            string message
        )
        {
            return new ResponseDto<T>
            {
                StatusCode = HttpStatusCode.BAD_REQUEST,
                Status = false,
                Message = message
            };
        }

        public static ResponseDto<T> NotFound<T>(
            string message
        )
        {
            return new ResponseDto<T>
            {
                StatusCode = HttpStatusCode.NOT_FOUND,
                Status = false,
                Message = message
            };
        }
    }
}