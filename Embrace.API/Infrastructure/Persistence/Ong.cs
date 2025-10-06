namespace Embrace.API.Infrastructure.Persistence
{
    public class Ong
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;

        public ICollection<AcaoSolidaria> AcoesSolidarias { get; set; } = [];
    }
}