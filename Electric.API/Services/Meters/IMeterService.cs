using Electric.API.Dtos.Common;
using Electric.API.Dtos.Meters;

namespace Electric.API.Services.Meters
{
    public interface IMeterService
    {
        Task<ResponseDto<ResponseMeterDto>> CreateAsync(CreateMeterDto dto);
    }
}