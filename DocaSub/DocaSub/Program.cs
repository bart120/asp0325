using Asp.Versioning;
using DocaSub.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//services
// Add services to the container.
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

app.UseAuthorization();

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

//app.MapControllers();
//app.MapRazorPages(); // razor pages

app.Run();
