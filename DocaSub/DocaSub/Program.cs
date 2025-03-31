var builder = WebApplication.CreateBuilder(args);
//services
// Add services to the container.
//builder.Services.AddRazorPages();// razor pages

builder.Services.AddControllersWithViews();/*.AddRazorOptions(options =>
{
    options.ViewLocationFormats.Clear();
    options.ViewLocationFormats.Add("MesVues/{1}/{0}.cshtml");
});*/ // mvc

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

//app.MapControllers();
app.MapControllerRoute( // mvc
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapRazorPages(); // razor pages

app.Run();
