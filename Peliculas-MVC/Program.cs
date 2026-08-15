using Microsoft.EntityFrameworkCore;
using Pelicula_MVC.Data;
using Peliculas_MVC.Data;
using Peliculas_MVC.Models;
using Microsoft.AspNetCore.Identity;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



//incluir contexto (dbcontext)
builder.Services.AddDbContext<PeliculaDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("PeliculaDbContext")));

//add identity
builder.Services.AddIdentityCore<Usuario>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 3;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PeliculaDbContext>()
    .AddSignInManager();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
})
    .AddIdentityCookies();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
    options.LoginPath = "/Usuario/Login";
    options.AccessDeniedPath = "/Usuario/AccessDenied";
});




var app = builder.Build();

// invocar la ejecucion del dbseeder con un using scope
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<PeliculaDbContext>();
        DbSeeder.Seed(context);

    }
    catch (Exception ex)
    {
        // Manejar la excepción si ocurre algún error durante la migración
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al aplicar las migraciones de la base de datos.");
    }
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
