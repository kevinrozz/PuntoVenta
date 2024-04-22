using AutoMapper;
using PuntoVenta.Dominio.Entity;

namespace PuntoVenta.Transversal.Mappers
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Usuario, UsuarioAux>().ReverseMap();
        }
    }
}