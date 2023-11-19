using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Entities;

public class ProductCategoryEntity
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;
}
