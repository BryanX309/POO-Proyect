using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Electric.API.Dtos.Meters;
using Electric.API.Entities;

namespace Electric.API.Mappers
{
    public class MeterMapper
    {
        public static MeterEntity CreateDtoToEntity(CreateMeterDto dto)
        {
            return new MeterEntity
            {
                Id = Guid.NewGuid().ToString(),
                SupplyKey = dto.SupplyKey,
                ClientId = dto.ClientId,
                ConsumptionType = dto.ConsumptionType,
                Rate = dto.Rate,
                ComercialSector = dto.ComercialSector,
                IsActive = true,
                CreatedById = "f51f7b25-0b93-46ce-be29-ca1db4762b77",
                CreatedDate = DateTime.Now
            };
        }

        public static MeterDto EntityToOneDto(MeterEntity entity)
        {
            return new MeterDto
            {
                Id = entity.Id,
                SupplyKey = entity.SupplyKey,
                ClientId = entity.ClientId,
                ConsumptionType = entity.ConsumptionType,
                Rate = entity.Rate,
                ComercialSector = entity.ComercialSector
            };
        }
    }
}