
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGatewayExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //[1]add ocelot 
            builder.Configuration.AddJsonFile("ocelot.json", optional:false, reloadOnChange: true);
            builder.Services.AddOcelot(builder.Configuration);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            //[2]add Ocelot
            app.UseOcelot().Wait();

            app.Run();
        }
    }
}
