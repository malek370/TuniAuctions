using System;

namespace backend.DTOs;

public class CreateProductDTO
{
    public string Name{get;set;}="";
    public float StartingPrice{get;set;}
    public string Description{get;set;}="";
}
