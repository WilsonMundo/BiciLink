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

namespace BusinessLogic.Services.SBicicleta
{
    public class StateBicycleService
    {
        private readonly IEstadoBicicletaRepository _stateBicycle;
        public StateBicycleService(IEstadoBicicletaRepository state)
        {
            this._stateBicycle = state;
        }
        public async Task<ResultAPI<GStateGeneralDTO>>  PostState(GEstadoDTO estadoDTO)
        {
            ResultAPI<GStateGeneralDTO> result = new ResultAPI<GStateGeneralDTO>(true);
            try
            {
                EstadoBicicletum estadoBicicletum = new EstadoBicicletum()
                {
                    Descripcion = estadoDTO.Descripcion,
                };

                var state = await _stateBicycle.Addasync(estadoBicicletum);

                result.result = new GStateGeneralDTO()
                {
                    descripcion = estadoDTO.Descripcion,
                    idEstado = state.EstadoBicicletaId
                };
                result.Ok(StatusHttpResponse.OK);
            }
            catch (Exception ex)
            {
                result.message = "Se producto un error al crear Estado Bicicleta notificar a soporte";
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
                var state = await  _stateBicycle.GetEstadoBicicletum(gStateGeneral.idEstado);
                if(state != null)
                {
                    state.Descripcion = gStateGeneral.descripcion;

                    bool procesado =  await _stateBicycle.SaveAsync();
                    if(procesado)
                    {
                        result.OkData(StatusHttpResponse.OK,new GStateGeneralDTO()
                        {
                            descripcion=state.Descripcion,
                            idEstado = state.EstadoBicicletaId,
                        });

                    }
                    else
                    {
                        result.error = true;
                        result.code = StatusHttpResponse.BadRequest;
                        result.message = "Error estado no existe";
                    }
                }
                else
                {
                    result.error = true;
                    result.code = StatusHttpResponse.BadRequest;
                    result.message = "Error estado no existe";
                }
            }
            catch (Exception ex)
            {
                result.message = "Se producto un error al Editar Estado Bicicleta notificar a soporte";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex.Message, "Editar Estado Bicicleta");
            }
            return result;
        }
        public async Task<ResultAPI<List<GStateGeneralDTO>>> GetState()
        {
            ResultAPI<List<GStateGeneralDTO>> result = new ResultAPI<List<GStateGeneralDTO>>(true);
            try
            {
                var data = await _stateBicycle.GetEstadoBicicletaAsync();
                result.result = data.Select(x =>  new GStateGeneralDTO()
                {
                    descripcion = x.Descripcion,
                    idEstado = x.EstadoBicicletaId,
                }).ToList();
                result.Ok(StatusHttpResponse.OK);

            }catch(Exception ex)
            {
                result.message = "Se producto un error al Listar Estado Bicicleta notificar a soporte";
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
                bool aplicado = await _stateBicycle.DeleteAsync(id);
                if(aplicado)
                {
                    result.Ok(StatusHttpResponse.OK);
                }
                else
                {
                    result.error = true;
                    result.code = StatusHttpResponse.BadRequest;
                    result.message = "Error estado no existe";
                }

            }catch(Exception ex)
            {
                result.message = "Se producto un error al eliminar Estado Bicicleta notificar a soporte";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex.Message);
            }
            return result;
        }
    }
}
