using System;
using Microsoft.AspNetCore.Identity;

namespace backend.Models;

public class AppUser:IdentityUser
{
    public List<Product> Products=[];
}
