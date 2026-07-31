using Electric.API.Dtos.Common;
using Microsoft.AspNetCore.Mvc;

namespace Electric.API.Controllers.Helpers
{
    public class CustomBaseController : ControllerBase
    {
        /// <summary>
        /// Mapea la information del response en un StatusCode del ActionResult 
        /// </summary>
        public ActionResult<ResponseDto<T>> ResponseStatus<T>(ResponseDto<T> response)
        {
            return StatusCode(response.StatusCode, new ResponseDto<T>
            {
                Status = response.Status,
                Message = response.Message,
                Data = response.Data
            });
        }
    }
}