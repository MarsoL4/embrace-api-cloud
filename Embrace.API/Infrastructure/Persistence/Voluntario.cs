namespace Embrace.API.Infrastructure.Persistence
{
    public class Voluntario
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;

        public ICollection<VoluntarioAcao> Acoes { get; set; } = [];
    }
}