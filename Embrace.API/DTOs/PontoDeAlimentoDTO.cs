namespace Embrace.API.DTOs
{
    public class PontoDeAlimentoDTO
    {
        public long Id { get; set; }
        public string NomeLocal { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Capacidade { get; set; }
    }
}