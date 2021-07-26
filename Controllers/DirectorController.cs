using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController] // Diz que a classe Controller é uma API
    [Route("[controller]")] // Rota do recurso
    public class DirectorController : ControllerBase
    {
        private readonly ApplicationDbContext _context; //Contexto intermediario para comunicação com o banco
        public DirectorController(ApplicationDbContext context) // Injeção de dependência -> pra construir um controller é necessario uma classe de contexto
        {
            _context = context;
        }

        //Get All- api/directors
        [HttpGet]
        public async Task<ActionResult<List<DirectorOutputGetAllDTO>>> Get()
        {
            var directors = await _context.Directors.ToListAsync();

            if (!directors.Any())
            {
                return NotFound("There are no directors registered!");
            }

            var directorOutputGetAllDto = new List<DirectorOutputGetAllDTO>();

            foreach (Director director in directors)
            {
                directorOutputGetAllDto.Add(new DirectorOutputGetAllDTO(director.Id, director.Name));
            }

            return directorOutputGetAllDto;


        }

        //GetByID - api/directors/1
        [HttpGet("{id}")] //É necessário informar que esse GET recebe o ID
        public async Task<ActionResult<DirectorOutputGetByIdDTO>> Get(long id)
        {


            var director = await _context.Directors.FirstOrDefaultAsync(director => director.Id == id);

            if (director == null)
            {
                return NotFound("Director not found!");
            }

            var directorOutputGetByIdDto = new DirectorOutputGetByIdDTO(director.Id, director.Name);
            return Ok(directorOutputGetByIdDto);


        }

        //POST -> api/directors
        [HttpPost]
        public async Task<ActionResult<DirectorOutputPostDTO>> Post([FromBody] DirectorInputPostDTO directorInputPostDto) // Vem do corpo da requisição 
        {
            var director = new Director(directorInputPostDto.Name); // A DTO limita o que eu sou obrigado a passar para o cadastro de diretor no swagger

            if (directorInputPostDto.Name == "")
            {
                return NotFound("Invalid director name!");
            }

            _context.Directors.Add(director);
            await _context.SaveChangesAsync();

            var directorOutputPostDto = new DirectorOutputPostDTO(director.Id, director.Name); //Saida mostrada no swagger(Id adicionado pelo save changes)
            return Ok(directorOutputPostDto);
        }

        //PUT -> api/directors
        [HttpPut("{id}")]
        public async Task<ActionResult<DirectorOutputPutDTO>> Put(long id, [FromBody] DirectorInputPutDTO directorInputPut) //Toda vez que for async tem que ter uma Task
        {
            var director = new Director(directorInputPut.Name);

            if (id == 0)
            {
                return NotFound("Invalid director Id!");
            }
            if (directorInputPut.Name == "")
            {
                return NotFound("Invalid director name!");
            }

            director.Id = id;
            _context.Directors.Update(director);
            await _context.SaveChangesAsync();

            var directorOutputPutDto = new DirectorOutputPutDTO(director.Id, director.Name);
            return Ok(directorOutputPutDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {

            var director = await _context.Directors.FirstOrDefaultAsync(director => director.Id == id);

            if (director == null)
            {
                return NotFound("Director does not exists!");
            }
            _context.Remove(director);
            await _context.SaveChangesAsync();
            return Ok(director);


        }

    }
}

