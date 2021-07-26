using System;
using System.Net;
using System.Text.Json;
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
        try
        {
            await Next(context); //Encapsula todas as informações individuais do request 
        }
        catch (Exception e)
        {
            //throw new Exception("Unexpected Error! \nError: "+e.Message);
            await HandleExceptionAsync(context, e);
        }

    }

    private static Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        var code = HttpStatusCode.InternalServerError;
        if (e is ArgumentNullException)
        {
            code = HttpStatusCode.BadRequest;
        }

        context.Response.ContentType = "application/json"; //Confirma que a api sempre retornará um JSON
        context.Response.StatusCode = (int)code;
        // return context.Response.WriteAsJsonAsync(code); <- PESQUISAR
        return context.Response.WriteAsync(JsonSerializer.Serialize(new { error = e.Message })); //Padrao com o front-end
        //JSON Serialize -> converte um objeto c# em JSON
    }
}