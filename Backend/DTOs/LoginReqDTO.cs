using System;

namespace backend.DTOs;

public class LoginReqDTO
{
    public string Email{ get; set; }="";
    public string Password{ get; set; }="";
}
