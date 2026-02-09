using EmployeeService.Application.Services;
using EmployeeService.Domain.Repositories;
using EmployeeService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// Logging (Serilog)
// ----------------------------
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        path: "BizFlow/Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

builder.Host.UseSerilog();

// ----------------------------
// Services
// ----------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infrastructure (DbContext + Repositories)
builder.Services.AddInfrastructure(builder.Configuration);

// Application Services
builder.Services.AddScoped<IEmployeeAppService, EmployeeAppService>();

/// Problem Details (RFC 7807)
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;

        // Log 4xx errors as Information, 5xx as Error
        if (context.ProblemDetails.Status >= 500)
        {
            Log.Error("Server error {TraceId}: {@ProblemDetails}",
                context.HttpContext.TraceIdentifier,
                context.ProblemDetails);
        }
        else if (context.ProblemDetails.Status >= 400)
        {
            Log.Warning("Client error {TraceId}: {@ProblemDetails}",
                context.HttpContext.TraceIdentifier,
                context.ProblemDetails);
        }
    };
});

var app = builder.Build();

// ----------------------------
// Middleware
// ----------------------------

// Serilog HTTP request logging (must be first)
app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
    options.GetLevel = (httpContext, elapsed, ex) =>
    {
        if (ex != null || httpContext.Response.StatusCode >= 500)
            return LogEventLevel.Error;
        if (httpContext.Response.StatusCode >= 400)
            return LogEventLevel.Warning;
        return LogEventLevel.Information;
    };
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePages(); // Adds ProblemDetails for status codes like 404
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
