using Microsoft.EntityFrameworkCore;
using WebApplication1.DataModel;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            OData.Parser.ParseFilter("test gt 1234 and startswith(banana,'a') or x eq null");

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<MyContext>(options =>
            {
                // https://learn.microsoft.com/en-us/dotnet/api/microsoft.data.sqlclient.sqlconnection.connectionstring
                options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=WebApplication1;Integrated Security=True");

                options.AddInterceptors(new JsonDictionaryQueryExpressionInterceptor());
            });

            builder.Services.AddAutoMapper(config =>
            {
                config.AddProfile<MappingProfile>();
            });

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MyContext>();
                context.Database.EnsureCreated();
                DbInitializer.Initialize(context);
            }

            app.Run();
        }
    }
}