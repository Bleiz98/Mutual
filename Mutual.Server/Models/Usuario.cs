using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mutual.Server.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("password_hash")]
        public string PasswordHash { get; set; }

        [Column("rol")]
        public string Rol { get; set; }
    }
}
