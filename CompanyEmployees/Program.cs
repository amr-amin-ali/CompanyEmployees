using CompanyEmployees.Extensions;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(config => {
    //To allow content negotiaion via [Accept] header
    config.RespectBrowserAcceptHeader = true;
    //To reject content negotiaion for [Accept] header that the server does not support
    //for not supported [Accept] header, server respond with [406 Not Acceptable]
    config.ReturnHttpNotAcceptable = true;

    //to support patch req
    config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());


})
    .AddXmlDataContractSerializerFormatters()
    //Add my custom CSV output serializer
    .AddCustomCSVOutputFormatter()
    //Add reference to where the [controllers] are.
    .AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));


// to prevent [ApiController] attribute from responding 
//with [400 BadRequest] before hitting the controller
builder.Services.Configure<ApiBehaviorOptions>(options => {
    options.SuppressModelStateInvalidFilter = true;
});

//Services from my extension methods
builder.Services.AddCorsConfigurations();
builder.Services.AddRepositoryManager();
builder.Services.AddServiceManager();
builder.Services.AddSqlDbContext(builder.Configuration);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
builder.Services.AddLoggerConfigurations();



//NewtonsoftJson to support patch requests
/*
 * function configures support for JSON Patch using Newtonsoft.Json while leaving the other formatters unchanged. 
 */
NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() => new ServiceCollection()
    .AddLogging()
    .AddMvc()
    .AddNewtonsoftJson()
    .Services.BuildServiceProvider()
    .GetRequiredService<IOptions<MvcOptions>>()
    .Value.InputFormatters.OfType<NewtonsoftJsonPatchInputFormatter>()
    .First();




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
