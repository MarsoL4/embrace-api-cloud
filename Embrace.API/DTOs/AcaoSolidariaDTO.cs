namespace Embrace.API.DTOs
{
    public class AcaoSolidariaDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string TipoEvento { get; set; } = string.Empty; // ex: Enchente, Frio
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int MetaItens { get; set; }
        public long OngId { get; set; }
    }
}