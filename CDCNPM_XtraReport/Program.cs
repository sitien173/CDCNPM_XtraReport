using CDCNPM_XtraReport.Service;
using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Reporting;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDevExpressControls();

builder.Services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddSessionStateTempDataProvider()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

builder.Services.AddSession();
builder.Services.ConfigureReportingServices(configurator =>
{
    configurator.ConfigureWebDocumentViewer(viewerConfigurator =>
    {
        viewerConfigurator.UseCachedReportSourceBuilder();
    });
});

builder.Services.AddScoped<ISQLService, SQLService>();
builder.Services.AddScoped<IQueryService, QueryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        // using static System.Net.Mime.MediaTypeNames;
        context.Response.ContentType = "application/json";
        var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
        var statusCode = (int)HttpStatusCode.InternalServerError;
        var message = exceptionHandler?.Error.Message;
        var result = Newtonsoft.Json.JsonConvert.SerializeObject(new { statusCode = statusCode, message = message });
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(result);
    });
});

app.UseSession();
app.UseMvcWithDefaultRoute();
app.UseDevExpressControls();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapGet("/", context =>
    {
        return Task.Run(() => context.Response.Redirect("/Home"));
    });
});
app.Run();