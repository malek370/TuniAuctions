using System;
using Microsoft.AspNetCore.Identity;

namespace backend.Models;

public class Product
{
    public required IdentityUser Owner;
    public string OwnerId{get;set;}="";
    public string Id{get;set;}="";
    public string Name{get;set;}="";
    public float StartingPrice{get;set;}
    public string Description{get;set;}="";
}
