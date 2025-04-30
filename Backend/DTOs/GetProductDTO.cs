using System;

namespace backend.DTOs;

public class GetProductDTO
{
    public string OwnerId{get;set;}="";
    public string Id{get;set;}="";
    public string Name{get;set;}="";
    public float StartingPrice{get;set;}
    public string Description{get;set;}="";
    public bool IsSold{get;set;}
    public DateTime CreationDate{get;set;}
}
