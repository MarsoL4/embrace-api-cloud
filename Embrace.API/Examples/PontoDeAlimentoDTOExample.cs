using Embrace.API.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Embrace.API.Examples
{
    public class PontoDeAlimentoDTOExample : IExamplesProvider<PontoDeAlimentoDTO>
    {
        public PontoDeAlimentoDTO GetExamples()
        {
            return new PontoDeAlimentoDTO
            {
                NomeLocal = "Centro Comunitário Vila Nova",
                Endereco = "Rua das Flores, 123 - Vila Nova",
                Capacidade = 200
            };
        }
    }
}