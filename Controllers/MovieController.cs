using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly ApplicationDbContext _context; //Contexto intermediario para comunicação com o banco //Contexto intermediario para comunicação com o banco
        public MovieController(ApplicationDbContext context) // Injeção de dependência -> pra construir um controller é necessario uma classe de contexto
        {
            _context = context;
        }

        //Get All by ID - api/movies
        [HttpGet]
        public async Task<ActionResult<List<MovieOutputGetAllDTO>>> Get()
        {

            var movies = await _context.Movies.ToListAsync();

            if (!movies.Any())
            {
                throw new Exception("There are no movies registered!");
            }
            var movieOutputGetAllDto = new List<MovieOutputGetAllDTO>();
            foreach (Movie movie in movies)
            {
                movieOutputGetAllDto.Add(new MovieOutputGetAllDTO(movie.Id, movie.Title));
            }
            return movieOutputGetAllDto;


        }

        //GetByID - api/movies/1
        [HttpGet("{id}")] //É necessário informar que esse GET recebe o ID
        public async Task<ActionResult<MovieOutputGetByIdDTO>> Get(long id)
        {

            var movie = await _context.Movies.Include(movie => movie.Director).FirstOrDefaultAsync(movie => movie.Id == id);

                

            if (movie == null)
            {
                throw new Exception("Movie not found!");
            }

            var movieOutputGetByIdDto = new MovieOutputGetByIdDTO(movie.Id, movie.Title, movie.DirectorId, movie.Director.Name);
            return movieOutputGetByIdDto;

        }

        //POST -> api/movies
        [HttpPost]
        public async Task<ActionResult<MovieOutputPostDTO>> Post([FromBody] MovieInputPostDTO movieInputPostDto) // Vem do corpo da requisição 
        {   //Toda vez que for async tem que ter uma Task

            var director = await _context.Directors.FirstOrDefaultAsync(director => director.Id == movieInputPostDto.DirectorId); //Verifica se esse diretor existe( o ideal é não estar no controller )

            if (director == null)
            {
                throw new Exception("Director does not exists!");
            }

            var movie = new Movie(movieInputPostDto.Title, director.Id);
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var movieOutputPostDto = new MovieOutputPostDTO(movie.Id, movie.Title);

            return Ok(movieOutputPostDto);



        }

        //PUT -> api/movies
        [HttpPut("{id}")]
        public async Task<ActionResult<MovieOutputPutDTO>> Put(long id, [FromBody] MovieInputPutDTO movieInputPutDto)
        {


            var movie = new Movie(movieInputPutDto.Title, movieInputPutDto.DirectorId);

            if (id == 0)
            {
                throw new Exception("Invalid movie Id!");
            }
            if (movieInputPutDto.Title == "")
            {
                throw new Exception("Invalid director name!");
            }

            movie.Id = id;
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();

            var movieOutputPutDTO = new MovieOutputPutDTO(movie.Id, movie.Title);

            return Ok(movieOutputPutDTO);



        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {

            var movie = await _context.Movies.FirstOrDefaultAsync(movie => movie.Id == id);

            if (movie == null)
            {
                throw new Exception("Director does not exists!");
            }


            _context.Remove(movie);
            await _context.SaveChangesAsync();
            return Ok(movie);

        }

    }
}

