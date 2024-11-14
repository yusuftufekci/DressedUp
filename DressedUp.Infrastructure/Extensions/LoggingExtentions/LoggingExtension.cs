using Amazon;
using Amazon.CloudWatchLogs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.AwsCloudWatch;

namespace DressedUp.Infrastructure.Extensions.LoggingExtentions;

using Amazon;
using Amazon.CloudWatchLogs;
using Serilog;
using Serilog.Sinks.AwsCloudWatch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

public static class LoggingExtensions
{
public static void ConfigureSerilog(this IHostBuilder hostBuilder)
{
    hostBuilder.UseSerilog((context, services, loggerConfiguration) =>
    {
        var configuration = context.Configuration;

        if (context.HostingEnvironment.EnvironmentName == "Development")
        {
            var awsAccessKey = configuration["AWS:AccessKey"];
            var awsSecretKey = configuration["AWS:SecretKey"];
            var awsRegion = configuration["AWS:Region"] ?? "eu-north-1";

            var logGroupName = configuration["Serilog:LogGroupName"] ?? "/DressedUp-APP/Development";

            var cloudWatchOptions = new CloudWatchSinkOptions
            {
                LogGroupName = logGroupName,
                MinimumLogEventLevel = Serilog.Events.LogEventLevel.Debug,
                CreateLogGroup = true,
                TextFormatter = new Serilog.Formatting.Compact.RenderedCompactJsonFormatter()
            };

            var cloudWatchClient = new AmazonCloudWatchLogsClient(awsAccessKey, awsSecretKey, RegionEndpoint.GetBySystemName(awsRegion));

            loggerConfiguration
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.AmazonCloudWatch(cloudWatchOptions, cloudWatchClient);
        }
        else
        {
            var awsRegion = configuration["AWS:Region"] ?? "eu-north-1";
            var logGroupName = configuration["Serilog:LogGroupName"] ?? "/DressedUp-APP";

            var cloudWatchOptions = new CloudWatchSinkOptions
            {
                LogGroupName = logGroupName,
                MinimumLogEventLevel = Serilog.Events.LogEventLevel.Information,
                CreateLogGroup = true,
                TextFormatter = new Serilog.Formatting.Compact.RenderedCompactJsonFormatter()
            };

            var cloudWatchClient = new AmazonCloudWatchLogsClient(RegionEndpoint.GetBySystemName(awsRegion));

            loggerConfiguration
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.AmazonCloudWatch(cloudWatchOptions, cloudWatchClient);
        }
    });
}
}
