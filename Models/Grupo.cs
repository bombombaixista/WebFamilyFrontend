namespace WebFamilyFrontEnd.Models
{
    public class Grupo
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public List<Cliente>? Clientes { get; set; }
    }
}
