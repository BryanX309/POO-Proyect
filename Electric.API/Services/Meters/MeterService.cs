using Electric.API.Constants;
using Electric.API.Database;
using Electric.API.Dtos.Common;
using Electric.API.Dtos.Meters;
using Electric.API.Entities;
using Electric.API.Helpers;
using Electric.API.Mappers;
using Microsoft.AspNetCore.Mvc;
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

            if (meterEntity is null)
            {
                return ResponseHelper.NotFound<MeterDto>("Registro no encontrado");
            }

            return ResponseHelper.OK<MeterDto>("Registro Encontrado", MeterMapper.EntityToOneDto(meterEntity));
        }

        public async Task<ResponseDto<PageDto<List<MeterDto>>>> GetPagesAsync(
            string supplyKey = "", //Búsquedas directas de la Clave de Suministro 
            string clientId = "", //Búsquedas de Contadores del Mismo Cliente
            string comercialSector = "", //Búsquedas dentro del mismo Sector Comercial
            string searchTerm = "", //Búsquedas de un termino en cualquier campo
            int page = 1,
            int pageSize = 10)
        {
            page = Math.Abs(page); //Asegura que la pagina sea positiva (-2) -> 2
            pageSize = Math.Abs(pageSize);

            pageSize = pageSize <= 0 ? PAGE_SIZE : pageSize;
            pageSize = pageSize > PAGE_SIZE_LIMIT ? PAGE_SIZE_LIMIT : pageSize;

            IQueryable<MeterEntity> meterQuery = _context.Meters;

            //Filtrando por Termino de Búsqueda
            if (!string.IsNullOrEmpty(searchTerm))
            {
                meterQuery = meterQuery
                    .Where(c => (c.SupplyKey +" "+ c.ClientId +" "+ c.ComercialSector +" "+ c.ConsumptionType +" "+c.Rate)
                    .ToLower().Contains(searchTerm.ToLower()));
            }

            //Filtrando por Clave de Suministro
            if (!string.IsNullOrEmpty(supplyKey))
            {
                meterQuery = meterQuery
                    .Where(c => c.SupplyKey == supplyKey);
            }

            //Filtrando por Id de Cliente
            if (!string.IsNullOrEmpty(clientId))
            {
                meterQuery = meterQuery
                    .Where(c => c.ClientId == clientId);
            }

            //Filtrando por Sector Comercial
            if (!string.IsNullOrEmpty(comercialSector))
            {
                meterQuery = meterQuery
                    .Where(c => c.ComercialSector == comercialSector);
            }



            int totalRows = await meterQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            page = page > totalPages ? 1 : page;

            int startIndex = (page - 1) * pageSize;

            var metersEntity = await meterQuery
                .OrderBy(c => c.SupplyKey)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            return ResponseHelper.OK<PageDto<List<MeterDto>>>(
                totalRows > 0 ? "Registros Encontrados" : "Ninguna Coincidencia Encontrada",
                new PageDto<List<MeterDto>>
                {
                    CurrentPage = page == 0 ? 1 : page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = totalPages,
                    HasNextPage = page < totalPages,
                    HasPreviousPage = page > 1,
                    Items = MeterMapper.ListEntityToListDto(metersEntity)
                });
        }

        public async Task<ResponseDto<ResponseMeterDto>> EditAsync(string id, EditMeterDto dto)
        {
            var meterEntity = await _context.Meters.FirstOrDefaultAsync(c => c.Id == id);

            if(meterEntity is null)
            {
                ResponseHelper.NotFound<ResponseMeterDto>("Registro no Encontrado");
            }

            var meterEntityUpdate = MeterMapper.EditDtoToEntity(dto, meterEntity);

            _context.Meters.Update(meterEntityUpdate);

            await _context.SaveChangesAsync();

            return ResponseHelper.OK<ResponseMeterDto>("Registro Modificado Correctamente", new ResponseMeterDto
            {
                Id = id
            });
        }

        public async Task<ResponseDto<ResponseMeterDto>> DeleteAsync(string id)
        {
            var meterEntity = await _context.Meters.FirstOrDefaultAsync(c => c.Id == id);

            if(meterEntity is null)
            {
                ResponseHelper.NotFound<ResponseMeterDto>("Registro no Encontrado");
            }

            _context.Meters.Remove(meterEntity);

            await _context.SaveChangesAsync();

            return ResponseHelper.OK<ResponseMeterDto>("Registro Eliminado Correctamente", new ResponseMeterDto
            {
                Id = id
            });
        }
    }
}