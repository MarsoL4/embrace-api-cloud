namespace Embrace.API.Infrastructure.Persistence
{
    public class VoluntarioAcao
    {
        public long VoluntarioId { get; set; }
        public Voluntario Voluntario { get; set; } = null!;

        public long AcaoSolidariaId { get; set; }
        public AcaoSolidaria AcaoSolidaria { get; set; } = null!;
    }
}