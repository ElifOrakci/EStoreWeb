using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using QuasarShop;
using QuasarShopData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder
    .Services
    .AddDbContext<AppDbContext>(config =>
    {
        var dbProvider = builder.Configuration.GetValue<string>("DbProvider");
        var cs = builder.Configuration.GetConnectionString(dbProvider!);
        switch (dbProvider ) 
        {
            case "PostgreSql":
                config.UseNpgsql(cs,p=> p.MigrationsAssembly("MigrationsPostgreSql"));
                break;
            case "MySql":
                config.UseMySql(cs, ServerVersion.AutoDetect(cs), p => p.MigrationsAssembly("MigrationsMySql"));
                break;
            case "SqlServer":
                default:
                config.UseSqlServer(cs, p => p.MigrationsAssembly("MigrationsSqlServer"));
                break;

        }

    });

builder.Services.AddIdentity<User, Role>(config =>
{
    config.SignIn.RequireConfirmedEmail = true;

    config.Password.RequireDigit = builder.Configuration.GetValue<bool>("Security:Password:RequireDigit");
    config.Password.RequiredLength = builder.Configuration.GetValue<int>("Security:Password:RequiredLength");
    config.Password.RequiredUniqueChars = builder.Configuration.GetValue<int>("Security:Password:RequiredUniqueChars");
    config.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Security:Password:RequireLowercase");
    config.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Security:Password:RequireUppercase");
    config.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Security:Password:RequireNonAlphanumeric");

    config.Lockout.MaxFailedAccessAttempts = builder.Configuration.GetValue<int>("Security:Lockout:MaxFailedAccessAttempts");
    config.Lockout.DefaultLockoutTimeSpan = builder.Configuration.GetValue<TimeSpan>("Security:Lockout:DefaultLockoutTimeSpan");

     
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<AppIdentityErrorDescriber>();

builder
    .Services
    .AddMailKit(optionBuilder =>
    {
        optionBuilder.UseMailKit(new MailKitOptions()
        {
            //get options from sercets.json
            Server = builder.Configuration["EMail:Server"],
            Port = builder.Configuration.GetValue<int>("EMail:Port"),
            SenderName = builder.Configuration["EMail:SenderName"],
            SenderEmail = builder.Configuration["EMail:SenderEmail"],


            // can be optional with no authentication 
            Account = builder.Configuration["EMail:Account"],
            Password = builder.Configuration["EMail:Password"],
            // enable ssl or tls
            Security = builder.Configuration.GetValue<bool>("EMail:SSL")
        });
    });

builder.Services.AddScoped<ICatalogsService,CatalogsService>();
builder.Services.AddScoped<IFilesService, FileService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICarouselImageService, CarouselImageService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseQuasarsShop();

app.Run();


 