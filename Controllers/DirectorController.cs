using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIProject.Models;

[ApiController] // Diz que a classe Controller é uma API
[Route("[controller]")] // Rota do recurso
public class DirectorController : ControllerBase
{
    private readonly ApplicationDbContext _context; //Contexto intermediario para comunicação com o banco
    public DirectorController(ApplicationDbContext context) // Injeção de dependência -> pra construir um controller é necessario uma classe de contexto
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

    //Get All by ID
    [HttpGet]
    public async Task<List<Director>> Get()
    {
        return await _context.Directors.ToListAsync();
    }

    //GetByID
    [HttpGet("{id}")]
    public async Task<ActionResult<Director>> Get(long id)
    {
        var director = await _context.Directors.FirstOrDefaultAsync(director => director.Id == id);
        return Ok(director);
    }
        

    [HttpPut("{id}")]
    public async Task<ActionResult<Director>> Put(long id, [FromBody] Director director)
    {
        director.Id = id;
        _context.Directors.Update(director);
        await _context.SaveChangesAsync();

        return Ok(director);
    }

    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
        var director = await _context.Directors.FirstOrDefaultAsync(director => director.Id == id);
        _context.Remove(director);
        await _context.SaveChangesAsync();
        return Ok(director);
    }


}