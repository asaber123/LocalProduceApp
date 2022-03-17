using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LocalProduceApp.Data;

var builder = WebApplication.CreateBuilder(args);

//Database connection configurations to sqlLite, also adding a connection string. 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LocalProduceAppIdentityDbContext>(options =>
    options.UseSqlServer(connectionString)); builder.Services.AddDbContext<ApplicationDbContext>(options =>
     options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

//Database connection configurations to sqlLite, for LocalProduce tables put in a database called API, also adding a connection string. 
builder.Services.AddDbContext<LocalProduceAppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("LocalProduceAppConnectionString")));

/* enable cors */
builder.Services.AddCors(opt => opt.AddPolicy("CorsPolicy", c =>
{
    c.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();
}));


var app = builder.Build();






// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

/* enable cors */
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

//This is for trhe routing configurations
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
