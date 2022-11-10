namespace PinaryDevelopment.Git.Server.Apis
{
    using Services;
    using DataAccess;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHealthChecks();
            builder.Services.AddControllers();
            builder.Services
                    .AddEndpointsApiExplorer()
                    .AddSwaggerGen()
                    .RegisterServicesDependencies()
                    .RegisterDataAccessDependencies();

            var app = builder.Build();

            app.MapHealthChecks("/status");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}