using Embrace.API.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Embrace.API.Examples
{
    public class VoluntarioDTOExample : IExamplesProvider<VoluntarioDTO>
    {
        public VoluntarioDTO GetExamples()
        {
            return new VoluntarioDTO
            {
                Nome = "João da Silva",
                Telefone = "(11) 99999-8888",
                Cidade = "São Paulo"
            };
        }
    }
}