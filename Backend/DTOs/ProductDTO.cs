using System;

namespace backend.DTOs;

public class ProductDTO
{
    public string Id{get;set;}="";
    public string Name{get;set;}="";
    public float StartingPrice{get;set;}
    public string Description{get;set;}="";
    public bool IsSold{get;set;}=false;
    public DateTime CreationDate{get;set;}=DateTime.UtcNow;
}
