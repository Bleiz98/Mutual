namespace Mutual.Server.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class PersonaRegistro
    {
        [Required(ErrorMessage = "El DNI/CUIT es obligatorio.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El DNI/CUIT solo puede contener números.")]
        public int DniCuit { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [MinLength(6, ErrorMessage = "El nombre de usuario debe tener al menos 6 caracteres.")]
        [RegularExpression(@"^(?=.*[0-9\W]).+$", ErrorMessage = "El nombre de usuario debe incluir números o símbolos.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*[\d\W]).+$", ErrorMessage = "La contraseña debe contener letras y al menos un número o símbolo.")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        [RegularExpression("^(Admin|Socio|Proovedor)$", ErrorMessage = "Rol inválido. Solo se permiten: Admin, Socio, Proovedor.")]
        public string Rol { get; set; }
    }
}
