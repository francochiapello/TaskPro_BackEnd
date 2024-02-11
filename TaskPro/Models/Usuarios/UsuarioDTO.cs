namespace TaskPro.Models.Usuarios
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Email { get; set; }

        public string? Contraseña { get; set; }
    }
    public class LoginDTO
    {
        public string Email { get; set; }

        public string Contraseña { get; set; }
    }
    public class RegisterDTO
    {
        public string Nombre { get; set; }

        public string Email { get; set; }

        public string Contraseña { get; set; }
    }

    public class  CreateUsuarioDTO : RegisterDTO
    {

    }
    public class UpdateUsuarioDTO : RegisterDTO
    {

    }
}
