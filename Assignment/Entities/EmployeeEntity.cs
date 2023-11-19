using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Entities;

public class EmployeeEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    
    [Column(TypeName = "money")]
    public decimal Salary { get; set; }
    public int AddressId { get; set; }
    public AddressEntity Address { get; set; } = null!;
}
