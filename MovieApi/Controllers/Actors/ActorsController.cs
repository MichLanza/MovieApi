using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Db;
using MovieApi.Dtos.Actors;
using MovieApi.Entities;

namespace MovieApi.Controllers.Actors
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public ActorsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _context.Actors.ToListAsync();
            var response = _mapper.Map<List<ActorDto>>(list);
            return Ok(response);
        }

        [HttpGet("{id:guid}", Name = "GetActorById")]
        public async Task<IActionResult> Get(Guid id)
        {

            var actor = await _context.Actors.FirstOrDefaultAsync(actor => actor.Id == id);

            if (actor is null)
            {
                return NotFound(new { message = "Actor no encontrado" });
            }
            var response = _mapper.Map<Actor>(actor);
            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, UpdateActorDto dto)
        {
            var exists = await _context.Actors.AnyAsync(actor => actor.Id == id);

            if (!exists)
            {
                return NotFound(new { message = "Actor no encontrado" });
            }
            var actor = _mapper.Map<Actor>(dto);
            actor.Id = id;
            _context.Entry(actor).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateActorDto dto)
        {
            var actor = _mapper.Map<Actor>(dto);
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<ActorDto>(actor);
            return new CreatedAtRouteResult("GetActorById", new { id = response.Id }, response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var exists = await _context.Actors.AnyAsync(actor => actor.Id == id);

            if (!exists)
            {
                return NotFound(new { message = "Actor no encontrado" });
            }

            _context.Actors.Remove(new Actor() { Id = id });

            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
