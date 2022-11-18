using AutoMapper;
using JuegosApi.DTOs;
using JuegosApi.Entidades;

namespace JuegosApi.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<JuegoDTO, Juego>();
            CreateMap<Juego, GetJuegoDTO>();
            CreateMap<Juego, JuegoDTOConDatos>()
                .ForMember(juegoDTO => juegoDTO.Datos, opciones => opciones.MapFrom(MapJuegoDTODatos));
            CreateMap<DatoCreacionDTO, Dato>()
                .ForMember(dato => dato.JuegoDato, opciones => opciones.MapFrom(MapJuegoDato));
            CreateMap<Dato, DatoDTO>();
            CreateMap<Dato, DatoDTOConJuegos>()
                .ForMember(datoDTO => datoDTO.Juegos, opciones => opciones.MapFrom(MapDatoDTOJuegos));
            CreateMap<DatoPatchDTO, Dato>().ReverseMap();
         
        }
        private List<DatoDTO> MapJuegoDTODatos(Juego juego, GetJuegoDTO getJuegoDTO)
        {
            var result = new List<DatoDTO>();

            if (juego.JuegoDato == null) { return result; }

            foreach (var JuegoDato in juego.JuegoDato)
            {
                result.Add(new DatoDTO()
                {
                    Id = JuegoDato.DatoId,
                    Genero = JuegoDato.Dato.Genero
                   
                });
            }

            return result;
        }

        private List<GetJuegoDTO> MapDatoDTOJuegos(Dato dato, DatoDTO DatoDTO)
        {
            var result = new List<GetJuegoDTO>();

            if (dato.JuegoDato == null)
            {
                return result;
            }

            foreach (var juegodato in dato.JuegoDato)
            {
                result.Add(new GetJuegoDTO()
                {
                    Id = juegodato.JuegoId,
                    Name = juegodato.Dato.Genero
                });
            }

            return result;
        }

        private List<JuegoDato> MapJuegoDato(DatoCreacionDTO datoCreacionDTO, Dato dato)
        {
            var resultado = new List<JuegoDato>();

            if (datoCreacionDTO.JuegosIds == null) { return resultado; }
            foreach (var juegoId in datoCreacionDTO.JuegosIds)
            {
                resultado.Add(new JuegoDato() { JuegoId = juegoId });
            }
            return resultado;
        }
    }
}