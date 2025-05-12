namespace Mutual.Server.Models
{
    public class UsuarioLogin
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; } // Esto es la contraseña ingresada
    }
}
