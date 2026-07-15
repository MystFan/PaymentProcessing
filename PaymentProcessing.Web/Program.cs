using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using MediatR.Pipeline;
using Microsoft.OpenApi;
using PaymentProcessing.Application;
using PaymentProcessing.DataAccess;
using PaymentProcessing.Web.Middlewares;

namespace PaymentProcessing.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddEnvironmentVariables();
            builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.SectionName));

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterModule(new WebModule());
            });

            builder.Services.AddDbContext<EFContext>();

            builder.Services.AddValidatorsFromAssemblyContaining<IApplicationAssemblyMarker>();

            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(IApplicationAssemblyMarker).Assembly);
                config.AddOpenBehavior(typeof(RequestPreProcessorBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(CommitBehavior<,>));
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddAuthorization();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "PaymentProcessing.Web"
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options => options.SwaggerEndpoint("v1/swagger.json", "PaymentProcessing.Web"));
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseAuthorization();

            app.MapEndpoints();

            app.Run();
        }
    }
}
