using System;

namespace backend.Models;

public class Transaction
{
    public Product? Product{get;set;}
    public string ProductId{get;set;}="";
    public AppUser? Buyer{get;set;}
    public string BuyerId{get;set;}="";
    public float FinalPrice{get;set;}
}
