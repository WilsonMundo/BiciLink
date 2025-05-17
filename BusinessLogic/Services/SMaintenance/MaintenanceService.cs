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

namespace BusinessLogic.Services.SMaintenance
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IMaintenanceRepository _maintenance;

        public MaintenanceService(IMaintenanceRepository maintenance)
        {
            this._maintenance = maintenance;
           
        }
        public async Task<ResultAPI<GStateGeneralDTO>> PostState(GEstadoDTO estadoDTO)
        {
            ResultAPI<GStateGeneralDTO> result = new ResultAPI<GStateGeneralDTO>(true);
            try
            {
                short consecutivo = await _maintenance.MaxIdAsync();
                EstadoMantenimiento estadoBicicletum = new EstadoMantenimiento()
                {
                    Descripcion = estadoDTO.Descripcion,
                    EstadoMantenimientoId = consecutivo,
                };

                var state = await _maintenance.Addasync(estadoBicicletum);

                result.result = new GStateGeneralDTO()
                {
                    descripcion = estadoDTO.Descripcion,
                    idEstado = state.EstadoMantenimientoId,
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
                var state = await _maintenance.GetEstadoMantenimiento(gStateGeneral.idEstado);
                if (state != null)
                {
                    state.Descripcion = gStateGeneral.descripcion;

                    bool procesado = await _maintenance.SaveAsync();
                    if (procesado)
                    {
                        result.OkData(StatusHttpResponse.OK, new GStateGeneralDTO()
                        {
                            descripcion = state.Descripcion,
                            idEstado = state.EstadoMantenimientoId,
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
                var data = await _maintenance.GetEstadoMantenimientoAsync();
                result.result = data.Select(x => new GStateGeneralDTO()
                {
                    descripcion = x.Descripcion,
                    idEstado = x.EstadoMantenimientoId,
                }).ToList();
                result.Ok(StatusHttpResponse.OK);

            }
            catch (Exception ex)
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
                bool aplicado = await _maintenance.DeleteAsync(id);
                if (aplicado)
                {
                    result.Ok(StatusHttpResponse.OK);
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
                result.message = "Se producto un error al eliminar Estado Bicicleta notificar a soporte";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex.Message);
            }
            return result;
        }
    }
}
