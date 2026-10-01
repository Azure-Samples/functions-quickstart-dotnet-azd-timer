using Azure.Monitor.OpenTelemetry.Exporter;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

if (!string.IsNullOrEmpty(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]))
{
    builder.Services
        .AddOpenTelemetry()
        .UseAzureMonitorExporter()
        .WithMetrics(metrics => metrics.AddMeter("Microsoft.Azure.Functions.Worker"))
        .WithTracing(tracing => tracing.AddSource("Microsoft.Azure.Functions.Worker"));
}

builder.Build().Run();
