using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using DocaSub.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

//services
// Add services to the container.

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "docaformation",
        ValidAudience = "docaformation",
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("unesupercledocaformation123456789!!!!!!!")),
    };
});

builder.Services.AddAuthorization();


builder.Services.AddRazorPages();// razor pages

builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});/*.AddRazorOptions(options =>
{
    options.ViewLocationFormats.Clear();
    options.ViewLocationFormats.Add("MesVues/{1}/{0}.cshtml");
});*/ // mvc
builder.Services.AddDbContext<DocaDbContext>(options =>
{
    if (builder.Environment.IsEnvironment("Testing"))
    {
        //options.UseInMemory("TestingDB");
    }
    else
    {
        options//.UseLazyLoadingProxies()
            .UseSqlServer(builder.Configuration.GetConnectionString("DocaConnection"));
    }
});

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    options.ApiVersionReader = new HeaderApiVersionReader();
    options.ApiVersionReader = new UrlSegmentApiVersionReader(); 
}).AddMvc();

/*builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});*/

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "DocaSub API",
        Version = "v1",
        Description = "API for DocaSub application"
    });
    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "DocaSub API",
        Version = "v2",
        Description = "API for DocaSub application"
    });
});

//builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        List<string> hosts = new List<string>();
        hosts.Add("http://monhostdoca.fr");
        List<string> methods = new List<string> { "GET", "POST", "PUT", "OPTIONS"};
        builder.WithOrigins(hosts.ToArray())
               .WithMethods(methods.ToArray())
               //.AllowAnyMethod()
               .AllowAnyHeader();
        /*builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();*/
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//déclaration midelwares
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication(); //oidc
app.UseAuthorization(); //oauth2

/*//app.MapControllers();
app.MapControllerRoute( // mvc
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");*/
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
     name: "default",
     pattern: "{controller=Home}/{action=Index}/{id?}"
   );

    endpoints.MapRazorPages();
});

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "DocaSub API V1");
    options.SwaggerEndpoint("/swagger/v2/swagger.json", "DocaSub API V2");
    options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});

//app.MapControllers();
//app.MapRazorPages(); // razor pages

app.Run();



public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) =>
        _provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, new OpenApiInfo
            {
                Title = $"My API {description.ApiVersion}",
                Version = description.ApiVersion.ToString()
            });
        }
    }
}