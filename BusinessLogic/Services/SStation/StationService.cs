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

namespace BusinessLogic.Services.SStation
{
    public class StationService: IStationService
    {
        IEstacionRepository _estacionRepository;
        private readonly IMapper _mapper;
        public StationService(IEstacionRepository estacionRepository, IMapper mapper)
        {
            this._estacionRepository = estacionRepository;
            this._mapper = mapper;
        }
        public async Task<ResultAPI<EstacionDTO>> InsertStation(EstacionDTO estacion)
        {

            ResultAPI<EstacionDTO> result = new ResultAPI<EstacionDTO>(true);
            try
            {
                estacion.Nombre = estacion.Nombre.Length > 50 ? estacion.Nombre.Substring(0, 50) : estacion.Nombre;
                estacion.Direccion = estacion.Direccion.Length > 250 ? estacion.Direccion.Substring(0, 250) : estacion.Direccion;
                Estacion estacion1 = _mapper.Map<Estacion>(estacion);

                estacion1 = await  _estacionRepository.Addasync(estacion1);

                result.OkData(StatusHttpResponse.OK, new EstacionDTO()
                {
                    Direccion = estacion1.Direccion,
                    estacionId = estacion1.EstacionId,
                    FlagActivo = estacion1.FlagActivo,
                    Latitud = estacion1.Latitud,
                    Longitud = estacion1.Longitud,
                    Nombre = estacion1.Nombre,
                });
            }
            catch(Exception ex)
            {
                result.message = "Se producto un error al crear estacion notificar a soporte";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex.Message);
            }
            return result;
        }
        public async Task<ResultAPI<IEnumerable<EstacionDTO>>> GetStation()
        {
            ResultAPI<IEnumerable<EstacionDTO>> result = new ResultAPI<IEnumerable<EstacionDTO>>(true);
            try
            {
                var stations =  await _estacionRepository.GetEstacions();
                if (stations.Count > 0)
                {

                    result.OkData(StatusHttpResponse.OK, stations.Select(x => new EstacionDTO()
                    {
                        Direccion = x.Direccion,
                        estacionId = x.EstacionId,
                        FlagActivo= x.FlagActivo,
                        Latitud= x.Latitud,
                        Longitud= x.Longitud,
                        Nombre = x.Nombre,
                        
                    }).ToList());
                    
                } else
                    result.Ok(StatusHttpResponse.NoContent);

            }catch(Exception ex )
            {
                result.message = "Se producto un error al consultar  estaciones notificar a soporte";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex.Message);
            }
            return result;
        }
    }
}
