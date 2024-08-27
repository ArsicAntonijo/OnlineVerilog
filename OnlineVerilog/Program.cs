using Microsoft.EntityFrameworkCore;
using OnlineVerilog.Context;
using OnlineVerilog.Service;
using Microsoft.AspNetCore.Identity;
using OnlineVerilog.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSession();
builder.Services.AddRazorPages();
builder.Services.AddTransient<VerilogHelper>();
builder.Services.AddDbContext<VeronContext>(
    options => options.UseMySql("",
    new MySqlServerVersion(new Version(8, 0, 28))));
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
    context.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Urls.Add("http://0.0.0.0:5000");

app.Run();
