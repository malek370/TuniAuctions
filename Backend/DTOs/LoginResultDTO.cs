using System;

namespace backend.DTOs;

public class LoginResultDTO
{
    public string Token{ get; set; }= "";
    public string Username{ get; set; }="";
}
