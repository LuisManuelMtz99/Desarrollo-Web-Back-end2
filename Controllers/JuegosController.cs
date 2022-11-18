using JuegosApi;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JuegosApi.DTOs;
using JuegosApi.Entidades;

namespace JuegosApi.Controllers
{
    [ApiController]
    [Route("juegos")]
    public class JuegosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public JuegosController(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            this.dbContext = context;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<GetJuegoDTO>>> Get()
        {
            var juegos = await dbContext.Juegos.ToListAsync();
            return mapper.Map<List<GetJuegoDTO>>(juegos);
        }


        [HttpGet("{id:int}", Name = "obtenerjuego")]
        public async Task<ActionResult<JuegoDTOConDatos>> Get(int id)
        {
            var juego = await dbContext.Juegos
                .Include(juegoDB => juegoDB.JuegoDato)
                .ThenInclude(juegoDatoDB => juegoDatoDB.Dato)
                .FirstOrDefaultAsync(juegoDB => juegoDB.Id == id);

            if (juego == null)
            {
                return NotFound();
            }

            return mapper.Map<JuegoDTOConDatos>(juego);

        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<GetJuegoDTO>>> Get([FromRoute] string nombre)
        {
            var juegos = await dbContext.Juegos.Where(juegoBD => juegoBD.Name.Contains(nombre)).ToListAsync();

            return mapper.Map<List<GetJuegoDTO>>(juegos);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] JuegoDTO juegoDto)
        {
            //Ejemplo para validar desde el controlador con la BD con ayuda del dbContext

            var existeJuegoMismoNombre = await dbContext.Juegos.AnyAsync(x => x.Name == juegoDto.Name);

            if (existeJuegoMismoNombre)
            {
                return BadRequest($"Ya existe un autor con el nombre {juegoDto.Name}");
            }

            var juego = mapper.Map<Juego>(juegoDto);

            dbContext.Add(juego);
            await dbContext.SaveChangesAsync();

            var juegoDTO = mapper.Map<GetJuegoDTO>(juego);

            return CreatedAtRoute("obtenerjuego", new { id = juego.Id }, juegoDTO);
        }

        [HttpPut("{id:int}")] // api/series/1
        public async Task<ActionResult> Put(JuegoDTO juegoCreacionDTO, int id)
        {
            var exist = await dbContext.Juegos.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            var juego = mapper.Map<Juego>(juegoCreacionDTO);
            juego.Id = id;

            dbContext.Update(juego);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Juegos.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El Recurso no fue encontrado.");
            }

            dbContext.Remove(new Juego()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}