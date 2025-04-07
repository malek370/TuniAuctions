using System;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace backend.Helpers;

public class CustomResponse<T>
{
    public  T? Data{ get; set; }
    public string Message{ get; set; } ="OK";
    public int ResponseStatus { get; set; }=200;
 
    public CustomResponse(T data,string message,int status)
    {
        Data=data;
        Message=message;
        ResponseStatus=status;

    }
    public CustomResponse(string message,int status)
    {
        Message=message;
        ResponseStatus=status;

    }
    public IResult HandleResponse()
    {
        return ResponseStatus switch
        {
            200 => Results.Ok(this),
            404 => Results.NotFound(this),
            400 => Results.BadRequest(this),
            401 => Results.Unauthorized(),
            403 => Results.Forbid(),
            409 => Results.Conflict(this),
            500 => Results.InternalServerError(this),
            _ => Results.StatusCode(500) // Default case
        };
    }
}
