using DocaSub.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//services
// Add services to the container.
//builder.Services.AddRazorPages();// razor pages

builder.Services.AddControllersWithViews();/*.AddRazorOptions(options =>
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
        options.UseSqlServer(builder.Configuration.GetConnectionString("DocaConnection"));
    }
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
});


//app.MapRazorPages(); // razor pages

app.Run();
