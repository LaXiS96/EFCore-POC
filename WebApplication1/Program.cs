using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication1.DataModel;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //OData.Parser.ParseFilter("test gt 1234 and startswith(banana,'a') or x eq null");

            DateTime? dt = DateTime.Parse("2023-01-01T00:00Z");
            DateTimeOffset dto = DateTimeOffset.Parse("2023-01-01T00:00Z");
            var res = Equals((DateTimeOffset?)dt, (DateTimeOffset?)dto);



            Convert(Expression.Parameter(typeof(DateTime)), Expression.Parameter(typeof(DateTime)));
            Convert(Expression.Parameter(typeof(DateTime?)), Expression.Parameter(typeof(DateTime)));
            Convert(Expression.Parameter(typeof(DateTime)), Expression.Parameter(typeof(DateTime?)));
            Convert(Expression.Parameter(typeof(DateTime?)), Expression.Parameter(typeof(DateTime?)));
            Convert(Expression.Parameter(typeof(DateTimeOffset)), Expression.Parameter(typeof(DateTimeOffset)));
            Convert(Expression.Parameter(typeof(DateTime)), Expression.Parameter(typeof(DateTimeOffset)));
            Convert(Expression.Parameter(typeof(DateTimeOffset)), Expression.Parameter(typeof(DateTime)));
            Convert(Expression.Parameter(typeof(DateTime?)), Expression.Parameter(typeof(DateTimeOffset)));

            void Convert(Expression left, Expression right)
            {
                var leftType = Nullable.GetUnderlyingType(left.Type) ?? left.Type;
                var rightType = Nullable.GetUnderlyingType(right.Type) ?? right.Type;
                var nullable = leftType != left.Type || rightType != right.Type;

                if (leftType == typeof(DateTime) && rightType == typeof(DateTimeOffset))
                    leftType = typeof(DateTimeOffset);
                else if (leftType == typeof(DateTimeOffset) && rightType == typeof(DateTime))
                    rightType = typeof(DateTimeOffset);

                // TODO here we could check if types are compatible and throw early

                var leftNewType = nullable ? typeof(Nullable<>).MakeGenericType(leftType) : leftType;
                var rightNewType = nullable ? typeof(Nullable<>).MakeGenericType(rightType) : rightType;

                if (leftNewType != left.Type)
                    left = Expression.Convert(left, leftNewType);
                if (rightNewType != right.Type)
                    right = Expression.Convert(right, rightNewType);
            }



            var param = Expression.Parameter(typeof(DateTime?), "$it");
            var cosnt = Expression.Constant(dto);
            var expr = Expression.MakeBinary(ExpressionType.Equal,
                Expression.Convert(param, typeof(DateTimeOffset?)),
                Expression.Convert(cosnt, typeof(DateTimeOffset?)));
            var predicate = Expression.Lambda<Func<DateTime?, bool>>(expr, param).Compile();

            var result = new[] {
                (DateTime?)DateTime.Parse("2023-01-01T00:00Z"),
                (DateTime?)DateTime.Parse("2022-01-01T00:00Z"),
            }.Where(predicate).ToArray();



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