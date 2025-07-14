using IAMNC0000N02S03.Business.EF.Services;
using IAMNC0000S03.Business.EF.Services;
using IAMNC0000S03.Business.Interfaces;
using IAMNC0000S03.Infrastructure;
using IAMNC0000S03.Repository;
using NLog;
using NLog.Web;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

DotNetEnv.Env.Load();
//LogHelper.LoadFromBase64(Environment.GetEnvironmentVariable("NLOG_CONFIG_BASE64"));
//var logger = LogManager.GetCurrentClassLogger();
var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();

logger.Debug("init main");
try
{
    //設定取得k8s相關資料
    GlobalDiagnosticsContext.Set("podid", Environment.GetEnvironmentVariable("POD_NAME") ?? "nuknown");
    GlobalDiagnosticsContext.Set("namespace", Environment.GetEnvironmentVariable("POD_NAMESPACE") ?? "default");
    GlobalDiagnosticsContext.Set("containerImage", Environment.GetEnvironmentVariable("CONTAINER_IMAGE") ?? "not-set");

    var builder = WebApplication.CreateBuilder(args);
    // Add services to the container.

    // 設定組態
    builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
    //.AddEnvironmentVariables();
    builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

    //將NLog註冊
    builder.Logging.ClearProviders();
    //設定Log記錄的最小等級
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    builder.Services.AddAuthorization();

    //註冊OracleDb
    builder.Services.AddSingleton<IOracleDbContext, OracleDbContext>();

    // 註冊 Repository
    builder.Services.AddSingleton<IIAMNC0000Repository, IAMNC0000Repository>();
    builder.Services.AddSingleton<IIAMNC0000N02Repository, IAMNC0000N02Repository>();

    // 註冊 Service
    builder.Services.AddSingleton<IIAMNC0000Service, IAMNC0000Service>();
    builder.Services.AddSingleton<IIAMNC0000N02Service, IAMNC0000N02Service>();

    //設定openTelemetry Resource
    var openTelemetryResource = ResourceBuilder.CreateDefault().AddService("QMA.E1000S01");

    builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
         .SetResourceBuilder(openTelemetryResource)
         .AddSource("MongoDB.Drive")
         .AddSource("OrcaleDBService")
         .SetSampler(new AlwaysOnSampler())
         .AddAspNetCoreInstrumentation(options =>
         {
             options.RecordException = true;
             options.EnrichWithHttpRequest = (activity, request) =>
             {
                 if (request.Headers.TryGetValue("http-Agent", out var userAgent))
                 {
                     activity.SetTag("http.user_agent", userAgent.ToString());
                 }
             };
             options.EnrichWithHttpResponse = (activity, response) =>
             {
                 activity.SetTag("http.response_length", response.ContentLength ?? 0);
             };
         })
         .AddHttpClientInstrumentation()
         .AddSqlClientInstrumentation(options =>
         {
             options.SetDbStatementForText = true;
             options.RecordException = true;
         })
         .AddConsoleExporter()
         .AddOtlpExporter(options =>
         {
             options.Endpoint = new Uri("http://localhost:4317");
         });
    });

    //設定 MongoDB ，讓OpenTelemetry 自動追蹤
    //var mongoClientSettings = MongoClientSettings.FromConnectionString("mongodb://admin:admin@192.168.84.150/");
    //builder.Services.AddSingleton<IMongoClient>(new MongoClient(mongoClientSettings));

    //-----
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // 配置CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin", policy =>
        {
            policy.WithOrigins("http://localhost:5000")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors("AllowSpecificOrigin");

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    //捕獲設定錯誤的記錄
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    //須確定在關閉時，把nlog關閉
    LogManager.Shutdown();
}