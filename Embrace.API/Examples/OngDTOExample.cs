using Embrace.API.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Embrace.API.Examples
{
    public class OngDTOExample : IExamplesProvider<OngDTO>
    {
        public OngDTO GetExamples()
        {
            return new OngDTO
            {
                Nome = "ONG Esperança",
                Cnpj = "12.345.678/0001-90",
                Email = "contato@ongesperanca.org",
                Telefone = "(11) 98765-4321"
            };
        }
    }
}