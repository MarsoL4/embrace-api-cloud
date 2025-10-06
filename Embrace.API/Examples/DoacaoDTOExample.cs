using Embrace.API.DTOs;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace Embrace.API.Examples
{
    public class DoacaoDTOExample : IExamplesProvider<DoacaoDTO>
    {
        public DoacaoDTO GetExamples()
        {
            return new DoacaoDTO
            {
                Tipo = "Alimento",
                Quantidade = 50,
                DataRecebida = new DateTime(2025, 6, 8, 10, 0, 0),
                AcaoSolidariaId = 1
            };
        }
    }
}