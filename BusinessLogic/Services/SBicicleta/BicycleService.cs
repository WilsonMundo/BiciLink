using AutoMapper;
using BusinessLogic.Interface;
using Domain.Interface.DataAcces;
using Domain.ModelContext;
using Domain.ModelsRegister;
using Serilog;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.SBicicleta
{
    public class BicycleService: IBicycleService
    {
        private readonly IBicicletaRepository _bicicletaRepository;
        private readonly IMapper _mapper;
        public BicycleService(IBicicletaRepository bicicletaRepository, IMapper mapper)
        {
            this._bicicletaRepository = bicicletaRepository;
            this._mapper = mapper;
        }

        public async Task<ResultAPI<BicycleDTO>> PostBicycle(BicycleDTO bicicletum)
        {
            ResultAPI<BicycleDTO> result = new ResultAPI<BicycleDTO>(true);
            try
            {
                if (bicicletum.Descripcion.Length > 450)
                    bicicletum.Descripcion = bicicletum.Descripcion.Substring(0, 450);
                if(!string.IsNullOrEmpty(bicicletum.Imagen))
                {
                    if(!HelpersService.EsUrlValida(bicicletum.Imagen))
                    {
                        result.code = StatusHttpResponse.BadRequest;
                        result.message = "ERROR. url de imagen invalida";
                        return result;
                    }
                   

                }
                if(!string.IsNullOrEmpty(bicicletum.Imagen1))
                {
                    if (!HelpersService.EsUrlValida(bicicletum.Imagen1))
                    {
                        result.code = StatusHttpResponse.BadRequest;
                        result.message = "ERROR. url de imagen invalida";
                        return result;
                    }
                  
                }

                var dbBicycle = _mapper.Map<Bicicletum>(bicicletum);
                var data =  await _bicicletaRepository.Addasync(dbBicycle);
                result.result = new BicycleDTO()
                {
                    bicycleId = data.BicicletaId,
                    Descripcion = data.Descripcion,
                    Caracteristica = data.Caracteristica,
                    EstadoBicicletaId = data.EstadoBicicletaId,
                    Imagen = data.Imagen,
                    Imagen1 = data.Imagen1,                    
                };
                
                result.Ok(StatusHttpResponse.OK);

            }
            catch (Exception ex)
            {
                result.message = "Se producto un error al crear bicicleta notificar a soporte";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex.Message);
            }
            return result;
        }

        public async Task<ResultAPI<List<BicycleDTO>>> GetBicycles()
        {
            ResultAPI<List<BicycleDTO>> result = new ResultAPI<List<BicycleDTO>>();
            try
            {
                var data = await _bicicletaRepository.GetBicycles();
                result.result = data.Select(x => new BicycleDTO()
                {
                    Caracteristica = x.Caracteristica,
                    Descripcion = x.Descripcion,
                    Imagen = x.Imagen,
                    Imagen1 = x.Imagen1,
                    bicycleId = x.BicicletaId,
                    EstadoBicicletaId = x.EstadoBicicletaId,
                    codigo = x.codigo,
                    EstadoString = x.EstadoBicicleta.Descripcion,
                    

                }
                ).ToList();
                result.Ok(StatusHttpResponse.OK);
            }
            catch (Exception ex)
            {
                result.message = "Se producto un error al crear bicicleta notificar a soporte";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex.Message);
            }
            return result;
        }
    }
}
