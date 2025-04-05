using System;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class RegisterDTO
{
    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters.")]
    public string UserName { get; set; }="";

    [Required]
    [EmailAddress]
    public string Email { get; set; }="";

    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
    public string Password { get; set; }="";

    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }="";
    [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression(@"^\d{8}$", ErrorMessage = "Phone number must be exactly 8 digits.")]
    public string PhoneNumber { get; set; }="";
}
