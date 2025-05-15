using AutoMapper;
using Domain.ModelContext;
using Domain.ModelsRegister;
using Shared.Auth.DTO;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserRegister, Usuario>()
                .ForMember(
                dest => dest.Email,
                opt => opt.MapFrom(src => src.Login)
                )
                .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.name)
                )
                .ForMember(
                dest => dest.Nit,
                opt => opt.MapFrom(src => src.nit)
                )
                .ForMember(
                dest => dest.Telefono,
                opt => opt.MapFrom(src => src.telefono)
                )
                .ForMember(
                dest => dest.Direccion,
                opt => opt.MapFrom(src => src.direccion)
                );
            CreateMap<BicycleDTO, Bicicletum>()
                .ForMember(
                dest => dest.EstadoBicicletaId,
                opt => opt.MapFrom(src => src.EstadoBicicletaId)
                )
                .ForMember(
                dest => dest.Descripcion,
                opt => opt.MapFrom(src => src.Descripcion)
                )
                .ForMember(
                dest => dest.Caracteristica,
                opt => opt.MapFrom(src => src.Caracteristica)
                )
                .ForMember(
                dest => dest.Imagen,
                opt => opt.MapFrom(src => src.Imagen)
                )
                .ForMember(
                dest => dest.Imagen1,
                opt => opt.MapFrom(src => src.Imagen1)
                );
            CreateMap<EstacionDTO, Estacion>();
        }
        
    }
}
