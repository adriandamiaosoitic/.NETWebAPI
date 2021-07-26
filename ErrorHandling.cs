using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate Next; // <-- Request 

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        this.Next = next;
    }

    public async Task Invoke(HttpContext context) //Método que passa o contexto para o dotnet
    {
        try{
            await Next(context); //Encapsula todas as informações individuais do request 
        }catch(Exception e){
            throw new Exception("Unexpected Error! \nError: "+e.Message);
        }
        
    }
}