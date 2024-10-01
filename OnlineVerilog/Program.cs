using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OnlineVerilog.Service;
using OnlineVerilog.Models;
using OnlineVerilog.Context;
using Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSession();
builder.Services.AddRazorPages();
builder.Services.AddTransient<VerilogHelper>();
builder.Services.AddTransient<IVeronRepository, VeronRepository>();
builder.Services.AddDbContext<VeronContext>(options => options.UseSqlite("Data Source=app.db"));
/*builder.Services.AddDbContext<VeronContext>(
    options => options.UseMySql("server=localhost;database=VeronDb;user=root;password=",
    new MySqlServerVersion(new Version(8, 0, 39))));*/
//builder.Services.AddDbContext<VeronContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddDefaultIdentity<User>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        // Configure password options
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 3;
        options.Password.RequiredUniqueChars = 0;
    })       
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<VeronContext>();

var app = builder.Build();

app.UseDeveloperExceptionPage();
// Configure the HTTP request pipeline.
/*if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}*/
//using(var scope = app.Services.CreateScope())
//{
//    var salesContext = scope.ServiceProvider.GetRequiredService<VeronContext>();
//    salesContext.Database.EnsureCreated();
//}
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<VeronContext>();
    await context.Database.MigrateAsync();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

//app.Urls.Add("http://0.0.0.0:5000");

app.Run();
