using System.ComponentModel.DataAnnotations;

namespace ProjectViews.Models;

public class TestValidate
{
    [Required] public int Id { get; set; }
    [StringLength(20)] public string Name { get; set; }
    [Required] public string Description { get; set; }

    [Range(15, 65, ErrorMessage = "Age must be between 15 and 65")]
    public int Age { get; set; }

    [Range(1000, 1000000, ErrorMessage = "Salary must be between 1000 and 1000000")]
    public long Salary { get; set; }

    [Required] public string Address { get; set; }
    [EmailAddress] public string Email { get; set; }
}