using AutoMapper;
using BusinessLogic.Interface;
using Domain.Interface;
using Domain.ModelContext;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.SMaintenance
{
    public class MaintenanceService: IMaintenanceService
    {
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly IMapper _mapper;
        public MaintenanceService(IMaintenanceRepository maintenance, IMapper mapper)
        {
            this._maintenanceRepository = maintenance;
            this._mapper = mapper;
        }

        public async Task<ResultAPI<MantenimientoDTO>> PostMaintenance(MantenimientoDTO mantenimientoDTO, long idUsuario)
        {
            var result = new ResultAPI<MantenimientoDTO>(true);

            try
            {
                var mantenimiento = _mapper.Map<Mantenimiento>(mantenimientoDTO);
                mantenimiento.TecnicoId = idUsuario;

                var data = await _maintenanceRepository.AddAsync(mantenimiento);

                result.result = new MantenimientoDTO
                {
                    MantenimientoId = data.MantenimientoId,
                    BicicletaId = data.BicicletaId,
                    Descripcion = data.Descripcion,
                    Observacion = data.Observacion,
                    FechaFin = data.FechaFin,
                    EstadoMantenimientoId = data.EstadoMantenimientoId,                   
                };

                result.Ok(StatusHttpResponse.OK);
            }
            catch (Exception ex)
            {
                result.message = "Se produjo un error al crear mantenimiento. Notificar a soporte.";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex.Message);
            }

            return result;
        }
        public async Task<ResultAPI<List<MantenimientoDTO>>> GetMaintenanceList()
        {
            var result = new ResultAPI<List<MantenimientoDTO>>();
            try
            {
                var data = await _maintenanceRepository.GetMantenimientos();

                result.result = data.Select(x => new MantenimientoDTO
                {
                    MantenimientoId = x.MantenimientoId,
                    BicicletaId = x.BicicletaId,
                    Descripcion = x.Descripcion,
                    Observacion = x.Observacion,
                    FechaFin = x.FechaFin,
                    EstadoMantenimientoId = x.EstadoMantenimientoId,
                    EstadoMantenimiento = x.EstadoMantenimiento?.Descripcion
                }).ToList();

                result.Ok(StatusHttpResponse.OK);
            }
            catch (Exception ex)
            {
                result.message = "Error al obtener lista de mantenimientos. Notificar a soporte.";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex.Message);
            }

            return result;
        }

        public async Task<ResultAPI<MantenimientoDTO>> GetMaintenanceById(long mantenimientoId)
        {
            var result = new ResultAPI<MantenimientoDTO>();
            try
            {
                var data = await _maintenanceRepository.GetMantenimiento(mantenimientoId);

                if (data == null)
                {
                    result.code = StatusHttpResponse.NotFound;
                    result.message = "Mantenimiento no encontrado.";
                    result.error = true;
                    return result;
                }

                result.result = new MantenimientoDTO
                {
                    MantenimientoId = data.MantenimientoId,
                    BicicletaId = data.BicicletaId,
                    Descripcion = data.Descripcion,
                    Observacion = data.Observacion,
                    FechaFin = data.FechaFin,
                    EstadoMantenimientoId = data.EstadoMantenimientoId,
                    EstadoMantenimiento = data.EstadoMantenimiento?.Descripcion
                };

                result.Ok(StatusHttpResponse.OK);
            }
            catch (Exception ex)
            {
                result.message = "Error al obtener mantenimiento. Notificar a soporte.";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex.Message);
            }

            return result;
        }
        public async Task<ResultAPI<MantenimientoDTO>> PutMaintenance(MantenimientoDTO mantenimientoDTO)
        {
            var result = new ResultAPI<MantenimientoDTO>(true);

            try
            {
                var existing = await _maintenanceRepository.GetMantenimiento(mantenimientoDTO.MantenimientoId);
                if (existing == null)
                {
                    result.code = StatusHttpResponse.NotFound;
                    result.message = "Mantenimiento no encontrado.";
                    result.error = true;
                    return result;
                }

                var entityToUpdate = _mapper.Map<Mantenimiento>(mantenimientoDTO);
                var updated = await _maintenanceRepository.UpdateAsync(entityToUpdate);

                if (updated == null)
                {
                    result.code = StatusHttpResponse.BadRequest;
                    result.message = "No se pudo actualizar el mantenimiento.";
                    result.error = true;
                    return result;
                }

                result.result = new MantenimientoDTO
                {
                    MantenimientoId = updated.MantenimientoId,
                    BicicletaId = updated.BicicletaId,
                    Descripcion = updated.Descripcion,
                    Observacion = updated.Observacion,
                    FechaFin = updated.FechaFin,
                    EstadoMantenimientoId = updated.EstadoMantenimientoId,
                    EstadoMantenimiento = updated.EstadoMantenimiento?.Descripcion
                };

                result.Ok(StatusHttpResponse.OK);
            }
            catch (Exception ex)
            {
                result.message = "Error al actualizar mantenimiento. Notificar a soporte.";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex.Message);
            }

            return result;
        }
        public async Task<ResultAPI<bool>> DeleteMaintenance(long mantenimientoId)
        {
            var result = new ResultAPI<bool>(true);

            try
            {
                var deleted = await _maintenanceRepository.DeleteAsync(mantenimientoId);
                if (!deleted)
                {
                    result.code = StatusHttpResponse.NotFound;
                    result.message = "Mantenimiento no encontrado o ya eliminado.";
                    result.error = true;
                    result.result = false;
                    return result;
                }

                result.result = true;
                result.Ok(StatusHttpResponse.OK);
            }
            catch (Exception ex)
            {
                result.message = "Error al eliminar mantenimiento. Notificar a soporte.";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                result.result = false;
                Log.Error(ex.Message);
            }

            return result;
        }
    }
}
