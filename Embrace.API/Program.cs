using Embrace.API.Infrastructure.Contexts;
using Embrace.API.AutoMapper;
using Embrace.API.Services;
using Microsoft.EntityFrameworkCore;
using Embrace.API.Infrastructure.Repositories;
using Embrace.API.Infrastructure.Repositories.Interfaces;
using Embrace.API.Repositories.Interfaces;
using Embrace.API.Repositories;
using Swashbuckle.AspNetCore.Filters;
using Embrace.API.Examples;

namespace Embrace.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuração de serviços
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            builder.Services.AddEndpointsApiExplorer();

            // Registra os exemplos para o Swagger
            builder.Services.AddSwaggerExamplesFromAssemblyOf<OngDTOExample>();

            builder.Services.AddSwaggerGen(options =>
            {
                options.ExampleFilters();
            });

            var connectionString = builder.Configuration.GetConnectionString("SqlServer");

            builder.Services.AddDbContext<EmbraceDbContext>(options =>
                options.UseSqlServer(connectionString));

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Repositories e Services
            builder.Services.AddScoped<IOngRepository, OngRepository>();
            builder.Services.AddScoped<OngService>();

            builder.Services.AddScoped<IVoluntarioRepository, VoluntarioRepository>();
            builder.Services.AddScoped<VoluntarioService>();

            builder.Services.AddScoped<IAcaoSolidariaRepository, AcaoSolidariaRepository>();
            builder.Services.AddScoped<AcaoSolidariaService>();

            builder.Services.AddScoped<IDoacaoRepository, DoacaoRepository>();
            builder.Services.AddScoped<DoacaoService>();

            builder.Services.AddScoped<IPontoDeAlimentoRepository, PontoDeAlimentoRepository>();
            builder.Services.AddScoped<PontoDeAlimentoService>();

            var app = builder.Build();

            // MIGRATION AUTOMÁTICA: Cria/atualiza tabelas no banco ao iniciar o App Service (Azure)
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<EmbraceDbContext>();
                db.Database.Migrate();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}