using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libertas.Source.Core.Entities.Models;

[Table("users")]
public class User
{
    [Required]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("first_name")]
    public string? FirstName { get; set; }

    [Required]
    [Column("last_name")]
    public string? LastName { get; set; }

    [Required]
    [Column("username")]
    public string? Username { get; set; }

    [Required]
    [Column("email")]
    public string? Email { get; set; }

    [Required]
    [Column("password")]
    public string? Password { get; set; }
}
