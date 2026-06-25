using System.ComponentModel.DataAnnotations;

namespace ContactApi.Models;

public class Contact
{
    public int Id { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = "First name must be at most 20 characters.")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(20, ErrorMessage = "Last name must be at most 20 characters.")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [StringLength(50, ErrorMessage = "Address must be at most 50 characters.")]
    public string Address { get; set; } = string.Empty;

    [Required]
    [StringLength(11, ErrorMessage = "Phone number must be exactly 11 digits.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be exactly 11 digits.")]
    public string PhoneNumber { get; set; } = string.Empty;
}
