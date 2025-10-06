using Embrace.API.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Embrace.API.Examples
{
    public class AcaoSolidariaDTOExample : IExamplesProvider<AcaoSolidariaDTO>
    {
        public AcaoSolidariaDTO GetExamples()
        {
            return new AcaoSolidariaDTO
            {
                Nome = "Campanha de Inverno",
                TipoEvento = "Arrecadação",
                Cidade = "Campinas",
                Estado = "SP",
                Descricao = "Campanha para arrecadação de agasalhos",
                MetaItens = 100,
                OngId = 1
            };
        }
    }
}