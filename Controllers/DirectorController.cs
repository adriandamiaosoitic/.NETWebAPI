using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController] // Diz que a classe Controller é uma API
[Route("[controller]")] // Rota do recurso
public class DirectorController : ControllerBase
{
    private readonly ApplicationDbContext _context; //Contexto intermediario para comunicação com o banco
    public DirectorController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Director>> Post([FromBody] Director director) // Vem do corpo da requisição 
    {   //Toda vez que for async tem que ter uma Task
         _context.Directors.Add(director);
         await _context.SaveChangesAsync();
         return Ok(director);
    }

    [HttpGet]
    public async Task<List<Director>> Get()
    {
        return await _context.Directors.ToListAsync();
    }
        

    [HttpPut]
    public string Put()
    {
        return "Put";
    }

    

    [HttpDelete]
    public string Delete()
    {
        return "Delete";
    }

}