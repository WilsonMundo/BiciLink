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
    public  class StateReservaService: IStateReservaService
    {
        private readonly IEstadoReservaRepository _estadoReservaRepository;

        public StateReservaService(IEstadoReservaRepository estadoReservaRepository)
        {
            this._estadoReservaRepository = estadoReservaRepository;
        }
        public async Task<ResultAPI<GStateGeneralDTO>> PostState(GEstadoDTO estadoDTO)
        {
            ResultAPI<GStateGeneralDTO> result = new ResultAPI<GStateGeneralDTO>(true);
            try
            {
                var estadoReserva = new EstadoReserva
                {
                    Descripcion = estadoDTO.Descripcion
                };

                var saved = await _estadoReservaRepository.AddAsync(estadoReserva);

                result.result = new GStateGeneralDTO
                {
                    descripcion = saved.Descripcion,
                    idEstado = saved.EstadoReservaId
                };

                result.Ok(StatusHttpResponse.OK);
            }
            catch (Exception ex)
            {
                result.message = "Se produjo un error al crear Estado Reserva. Notificar a soporte.";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex.Message);
            }

            return result;
        }

        public async Task<ResultAPI<GStateGeneralDTO>> PutState(GStateGeneralDTO gStateGeneral)
        {
            ResultAPI<GStateGeneralDTO> result = new ResultAPI<GStateGeneralDTO>(true);
            try
            {
                var estado = await _estadoReservaRepository.GetEstadoReservaByIdAsync(gStateGeneral.idEstado);
                if (estado != null)
                {
                    estado.Descripcion = gStateGeneral.descripcion;

                    bool procesado = await _estadoReservaRepository.SaveAsync();
                    if (procesado)
                    {
                        result.OkData(StatusHttpResponse.OK, new GStateGeneralDTO
                        {
                            descripcion = estado.Descripcion,
                            idEstado = estado.EstadoReservaId
                        });
                    }
                    else
                    {
                        result.error = true;
                        result.code = StatusHttpResponse.BadRequest;
                        result.message = "Error: estado no pudo ser actualizado.";
                    }
                }
                else
                {
                    result.error = true;
                    result.code = StatusHttpResponse.BadRequest;
                    result.message = "Error: estado no existe.";
                }
            }
            catch (Exception ex)
            {
                result.message = "Se produjo un error al editar Estado Reserva. Notificar a soporte.";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex.Message, "Editar Estado Reserva");
            }

            return result;
        }

        public async Task<ResultAPI<List<GStateGeneralDTO>>> GetState()
        {
            ResultAPI<List<GStateGeneralDTO>> result = new ResultAPI<List<GStateGeneralDTO>>(true);
            try
            {
                var data = await _estadoReservaRepository.GetEstadoReservasAsync();
                result.result = data.Select(x => new GStateGeneralDTO
                {
                    descripcion = x.Descripcion,
                    idEstado = x.EstadoReservaId
                }).ToList();

                result.Ok(StatusHttpResponse.OK);
            }
            catch (Exception ex)
            {
                result.message = "Se produjo un error al listar Estado Reserva. Notificar a soporte.";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex.Message);
            }

            return result;
        }

        public async Task<ResultAPI<object>> DeleteState(short id)
        {
            ResultAPI<object> result = new ResultAPI<object>(true);
            try
            {
                bool eliminado = await _estadoReservaRepository.DeleteAsync(id);
                if (eliminado)
                {
                    result.Ok(StatusHttpResponse.OK);
                }
                else
                {
                    result.error = true;
                    result.code = StatusHttpResponse.BadRequest;
                    result.message = "Error: estado no existe o ya fue eliminado.";
                }
            }
            catch (Exception ex)
            {
                result.message = "Se produjo un error al eliminar Estado Reserva. Notificar a soporte.";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex.Message);
            }

            return result;
        }

    }
}
