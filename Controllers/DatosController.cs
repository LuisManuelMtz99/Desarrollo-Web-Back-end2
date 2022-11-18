using JuegosApi;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JuegosApi.DTOs;
using JuegosApi.Entidades;

namespace JuegosApi.Controllers
{
    [ApiController]
    [Route("datos")]
    public class DatosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public DatosController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpGet("/listadoDato")]
        public async Task<ActionResult<List<Dato>>> GetAll()
        {
            return await dbContext.Datos.ToListAsync();
        }

        [HttpGet("{id:int}", Name = "obtenerDato")]
        public async Task<ActionResult<DatoDTOConJuegos>> GetById(int id)
        {
            var dato = await dbContext.Datos

                .Include(datoDB => datoDB.JuegoDato)
                .ThenInclude(juegoDatoDB => juegoDatoDB.Juego)
                //.Include(tipoDB => tipoDB.Tipos)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dato == null)
            {
                return NotFound();
            }

            dato.JuegoDato = dato.JuegoDato.OrderBy(x => x.Orden).ToList();

            return mapper.Map<DatoDTOConJuegos>(dato);
        }

        [HttpPost]
        public async Task<ActionResult> Post(DatoCreacionDTO datoCreacionDTO)
        {

            if (datoCreacionDTO.JuegosIds == null)
            {
                return BadRequest("No se puede crear datos sin juegos.");
            }

            var juegosIds = await dbContext.Juegos
                .Where(juegoBD => datoCreacionDTO.JuegosIds.Contains(juegoBD.Id)).Select(x => x.Id).ToListAsync();

            if (datoCreacionDTO.JuegosIds.Count != juegosIds.Count)
            {
                return BadRequest("No existe uno de los juegos enviados");
            }

            var dato = mapper.Map<Dato>(datoCreacionDTO);

            OrdenarPorJuegos(dato);

            dbContext.Add(dato);
            await dbContext.SaveChangesAsync();

            var datoDTO = mapper.Map<DatoDTO>(dato);

            return CreatedAtRoute("obtenerDato", new { id = dato.Id }, datoDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, DatoCreacionDTO datoCreacionDTO)
        {
            var datoDB = await dbContext.Datos
                .Include(x => x.JuegoDato)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (datoDB == null)
            {
                return NotFound();
            }

            datoDB = mapper.Map(datoCreacionDTO, datoDB);

            OrdenarPorJuegos(datoDB);

            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Datos.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El Recurso no fue encontrado.");
            }

            dbContext.Remove(new Dato { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        private void OrdenarPorJuegos(Dato dato)
        {
            if (dato.JuegoDato != null)
            {
                for (int i = 0; i < dato.JuegoDato.Count; i++)
                {
                    dato.JuegoDato[i].Orden = i;
                }
            }
        }

        //[HttpPatch("{id:int}")]
        // public async Task<ActionResult> Patch(int id, JsonPatchDocument<CategoriaPatchDTO> patchDocument)
        // {
        //   if (patchDocument == null) { return BadRequest(); }

        //   var categoriaDB = await dbContext.Categorias.FirstOrDefaultAsync(x => x.Id == id);

        //    if (categoriaDB == null) { return NotFound(); }

        //    var categoriaDTO = mapper.Map<CategoriaPatchDTO>(categoriaDB);

        //    patchDocument.ApplyTo(categoriaDTO);
        //
        //    var isValid = TryValidateModel(categoriaDTO);

        //    if (!isValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    mapper.Map(categoriaDTO, categoriaDB);

        //     await dbContext.SaveChangesAsync();
        //    return NoContent();
        // }
    }
}