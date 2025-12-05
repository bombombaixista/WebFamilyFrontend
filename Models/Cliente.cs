namespace WebFamilyFrontEnd.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int GrupoId { get; set; }
        public Grupo? Grupo { get; set; }
    }
}
