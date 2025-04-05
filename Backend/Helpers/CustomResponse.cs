using System;

namespace backend.Helpers;

public class CustomResponse<T>
{
    public  T? Data;
    public string Message ="OK";
    public int ResponseStatus =200;
 
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
    public IResult CreateResponse()
    {
        return ResponseStatus switch
        {
            200 => Results.Ok(Data),
            201 => Results.Created(Message, Data), // Requires a URI, use Results.Created(uri, data) if you have one
            400 => Results.BadRequest(new { Message }),
            401 => Results.Unauthorized(),
            403 => Results.Forbid(),
            404 => Results.NotFound(new { Message }),
            409 =>Results.Conflict(Message),
            500 => Results.StatusCode(500), // Use StatusCode for generic status codes
            _ => Results.StatusCode(ResponseStatus), // Default to the set status code
        };
    }
}
