using CompanyEmployees.Extensions;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(config => {
    //To allow content negotiaion via [Accept] header
    config.RespectBrowserAcceptHeader = true;
    //To reject content negotiaion for [Accept] header that the server does not support
    //for not supported [Accept] header, server respond with [406 Not Acceptable]
    config.ReturnHttpNotAcceptable = true;
})
    .AddXmlDataContractSerializerFormatters()
    //Add my custom CSV output serializer
    .AddCustomCSVOutputFormatter()
    //Add reference to where the [controllers] are.
    .AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

//Services from my extension methods
builder.Services.AddCorsConfigurations();
builder.Services.AddRepositoryManager();
builder.Services.AddServiceManager();
builder.Services.AddSqlDbContext(builder.Configuration);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
builder.Services.AddLoggerConfigurations();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.UseCustomExceptionHandlerMiddleware(logger);


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
