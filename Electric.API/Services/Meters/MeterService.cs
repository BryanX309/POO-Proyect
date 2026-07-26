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

        ResponseHelper responseHelper = new ResponseHelper();

        public MeterService(ElectricDbContext context, IConfiguration configuration)
        {
            _context = context;
        }

        //Create Meter
        public async Task<ResponseDto<ResponseMeterDto>> CreateAsync(CreateMeterDto dto)
        {

            var oldMeter = await _context.Meters.FirstOrDefaultAsync(c => c.SupplyKey == dto.SupplyKey);

            if (oldMeter is not null)
            {
                return ResponseHelper.BadRequest<ResponseMeterDto>($"La Clave de Distribution #{dto.SupplyKey} ya esta ocupa");   
            }
            ;

            MeterEntity meter = MeterMapper.CreateDtoToEntity(dto);

            _context.Meters.Add(meter);

            await _context.SaveChangesAsync();

            return ResponseHelper.Created<ResponseMeterDto>("Registro Ingresado Correctamente",
            new ResponseMeterDto
            {
                Id = meter.Id
            });
        }
    }
}