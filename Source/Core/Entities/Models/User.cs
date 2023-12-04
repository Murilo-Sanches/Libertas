using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Libertas.Source.Core.Entities.Models;

[Table("users")]
[Index(nameof(Username), IsUnique = true, Name = "uk_username")]
[Index(nameof(Email), IsUnique = true, Name = "uk_email")]
public class User
{
    [Key]
    [Required]
    [Column("id", TypeName = "bigint unsigned")]
    public ulong Id { get; set; }

    [Required]
    [Column("first_name")]
    [MaxLength(255)]
    public string? FirstName { get; set; }

    [Required]
    [Column("last_name")]
    [MaxLength(255)]
    public string? LastName { get; set; }

    [Required]
    [Column("username")]
    [MaxLength(255)]
    public string? Username { get; set; }

    [Required]
    [Column("email")]
    [MaxLength(255)]
    public string? Email { get; set; }

    [Required]
    [Column("password")]
    [MaxLength(255)]
    public string? Password { get; set; }
}
