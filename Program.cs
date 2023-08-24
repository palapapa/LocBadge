using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LocBadge;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHttpClient()
            .AddRouting()
            .AddOptions()
            .AddControllers();
        WebApplication app = builder.Build();
        app.MapControllers();
        app.UseDeveloperExceptionPage()
            .UseHsts()
            .UseHttpsRedirection()
            .UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("*"));
        app.Run();
    }
}
