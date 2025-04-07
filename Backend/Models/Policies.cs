using System;

namespace backend.Models;

public static class Policies
{
    public static string RequireBuyerRole{set;get;} = "RequireBuyerRole";
    public static string MemberRole{set;get;} = "MemberRole";
    public static string RequireSellerRole{set;get;} = "RequireSellerRole";


}