namespace SpMedicalGroup.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public required int RoleId { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }
}
