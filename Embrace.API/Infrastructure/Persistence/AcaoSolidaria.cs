namespace Embrace.API.Infrastructure.Persistence
{
    public class AcaoSolidaria
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string TipoEvento { get; set; } = string.Empty; // ex: Frio, Enchente
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int MetaItens { get; set; }

        public long OngId { get; set; }
        public Ong Ong { get; set; } = null!;

        public ICollection<Doacao> Doacoes { get; set; } = [];
        public ICollection<VoluntarioAcao> Voluntarios { get; set; } = [];
    }
}