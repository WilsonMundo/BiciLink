using AutoMapper;
using BusinessLogic.Interface;
using Domain.Interface.DataAcces;
using Domain.ModelContext;
using Serilog;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.SReserva
{
    public class ReservaService: IReservaService
    {
        private readonly IReservaRepository _reserva;
        private readonly IMapper _mapper;

        public ReservaService(IReservaRepository reservaRepository, IMapper mapper)
        {
            this._reserva = reservaRepository;
            this._mapper = mapper;
        }
        public async Task<ResultAPI<ReservaDTO>> PostReserva(ReservaDTO reservaDTO, long usuarioId)
        {
            var result = new ResultAPI<ReservaDTO>(true);

            try
            {

                if (reservaDTO.FechaReserva < DateTime.Now)
                {
                    result.message = "La fecha de reserva no puede ser menor a la fecha y hora actual.";
                    result.error = true;
                    result.code = StatusHttpResponse.BadRequest;
                    return result;
                }

                var reserva = _mapper.Map<Reserva>(reservaDTO);
                reserva.UsuarioId = usuarioId;
                reserva.EstadoReservaId = 1;
                reserva.CodigoDesbloqueo = Guid.NewGuid().ToString();
                var data = await _reserva.AddAsync(reserva);

                result.result = new ReservaDTO()
                {
                    BicicletaId = reserva.BicicletaId,
                    EstacionDestinoId = reserva.EstacionDestinoId,
                    EstadoReservaId = reserva.EstadoReservaId,
                    EstacionOrigenId = reserva.EstacionOrigenId,
                    FechaReserva = reserva.FechaReserva,
                    ReservaId = reserva.ReservaId,
                    CodigoDesbloqueo = reserva.CodigoDesbloqueo,                    

                };
                result.Ok(StatusHttpResponse.OK);
            }
            catch (Exception ex)
            {
                result.message = "Se produjo un error al crear la reserva. Notificar a soporte.";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex.Message);
            }

            return result;
        }

        public async Task<ResultAPI<List<ReservaDTO>>> GetReservaList()
        {
            var result = new ResultAPI<List<ReservaDTO>>();
            try
            {
                var data = await _reserva.GetReservas();

                result.result = data.Select(x => new ReservaDTO
                {
                    ReservaId = x.ReservaId,                    
                    BicicletaId = x.BicicletaId,
                    EstacionOrigenId = x.EstacionOrigenId,
                    EstacionDestinoId = x.EstacionDestinoId,
                    CodigoDesbloqueo = x.CodigoDesbloqueo,
                    FechaReserva = x.FechaReserva,
                    EstadoReservaId = x.EstadoReservaId,                  
                }).ToList();

                result.Ok(StatusHttpResponse.OK);
            }
            catch (Exception ex)
            {
                result.message = "Error al obtener lista de reservas. Notificar a soporte.";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex.Message);
            }

            return result;
        }

        public async Task<ResultAPI<ReservaDTO>> GetReservaById(long reservaId)
        {
            var result = new ResultAPI<ReservaDTO>();
            try
            {
                var data = await _reserva.GetReserva(reservaId);

                if (data == null)
                {
                    result.code = StatusHttpResponse.NotFound;
                    result.message = "Reserva no encontrada.";
                    result.error = true;
                    return result;
                }

                result.result = new ReservaDTO
                {
                    ReservaId = data.ReservaId,                    
                    BicicletaId = data.BicicletaId,
                    EstacionOrigenId = data.EstacionOrigenId,
                    EstacionDestinoId = data.EstacionDestinoId,
                    CodigoDesbloqueo = data.CodigoDesbloqueo,
                    FechaReserva = data.FechaReserva,
                    EstadoReservaId = data.EstadoReservaId,                  
                };

                result.Ok(StatusHttpResponse.OK);
            }
            catch (Exception ex)
            {
                result.message = "Error al obtener reserva. Notificar a soporte.";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex.Message);
            }

            return result;
        }

        public async Task<ResultAPI<ReservaDTO>> PutReserva(ReservaDTO reservaDTO)
        {
            var result = new ResultAPI<ReservaDTO>(true);

            try
            {
                var existing = await _reserva.GetReserva(reservaDTO.ReservaId);
                if (existing == null)
                {
                    result.code = StatusHttpResponse.NotFound;
                    result.message = "Reserva no encontrada.";
                    result.error = true;
                    return result;
                }

                var entityToUpdate = _mapper.Map<Reserva>(reservaDTO);
                var updated = await _reserva.UpdateAsync(entityToUpdate);

                if (updated == null)
                {
                    result.code = StatusHttpResponse.BadRequest;
                    result.message = "No se pudo actualizar la reserva.";
                    result.error = true;
                    return result;
                }

                result.result = _mapper.Map<ReservaDTO>(updated);
                result.Ok(StatusHttpResponse.OK);
            }
            catch (Exception ex)
            {
                result.message = "Error al actualizar reserva. Notificar a soporte.";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex.Message);
            }

            return result;
        }

        public async Task<ResultAPI<bool>> DeleteReserva(long reservaId)
        {
            var result = new ResultAPI<bool>(true);

            try
            {
                var deleted = await _reserva.DeleteAsync(reservaId);
                if (!deleted)
                {
                    result.code = StatusHttpResponse.NotFound;
                    result.message = "Reserva no encontrada o ya eliminada.";
                    result.error = true;
                    result.result = false;
                    return result;
                }

                result.result = true;
                result.Ok(StatusHttpResponse.OK);
            }
            catch (Exception ex)
            {
                result.message = "Error al eliminar reserva. Notificar a soporte.";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                result.result = false;
                Log.Error(ex.Message);
            }

            return result;
        }

    }
}
