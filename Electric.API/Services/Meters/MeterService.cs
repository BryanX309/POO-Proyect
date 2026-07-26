using Electric.API.Constants;
using Electric.API.Database;
using Electric.API.Dtos.Common;
using Electric.API.Dtos.Meters;
using Electric.API.Entities;
using Electric.API.Helpers;
using Electric.API.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Electric.API.Services.Meters
{
    public class MeterService : IMeterService
    {
        private readonly ElectricDbContext _context;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public MeterService(ElectricDbContext context, IConfiguration configuration)
        {
            _context = context;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }

        //Create Meter
        public async Task<ResponseDto<ResponseMeterDto>> CreateAsync(CreateMeterDto dto)
        {

            var oldMeter = await _context.Meters.FirstOrDefaultAsync(c => c.SupplyKey == dto.SupplyKey);

            if (oldMeter is not null)
            {
                return ResponseHelper.BadRequest<ResponseMeterDto>($"La Clave de Distribution #{dto.SupplyKey} ya esta ocupa");   
            }

            MeterEntity meter = MeterMapper.CreateDtoToEntity(dto);

            _context.Meters.Add(meter);

            await _context.SaveChangesAsync();

            return ResponseHelper.Created<ResponseMeterDto>("Registro Ingresado Correctamente",
            new ResponseMeterDto
            {
                Id = meter.Id
            });
        } 

        public async Task<ResponseDto<MeterDto>> GetOneByIdAsync(string id)
        {
            var meterEntity = await _context.Meters.FirstOrDefaultAsync(c => c.Id == id);

            if(meterEntity is null)
            {
                return ResponseHelper.NotFound<MeterDto>("Registro no encontrado");
            }

            return ResponseHelper.OK<MeterDto>("Registro Encontrado", MeterMapper.EntityToOneDto(meterEntity));
        }

        public async Task<ResponseDto<PageDto<List<MeterDto>>>> GetPagesAsync(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }
        public async Task<ResponseDto<ResponseMeterDto>> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto<ResponseMeterDto>> EditAsync(string id, EditMeterDto dto)
        {
            throw new NotImplementedException();
        }
    }
}