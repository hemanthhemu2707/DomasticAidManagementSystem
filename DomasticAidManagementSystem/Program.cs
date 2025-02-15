using DomasticAidManagementSystem;
using IIITS.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15); 
    options.Cookie.HttpOnly = true; 
    options.Cookie.IsEssential = true; 
});

builder.Services.AddDbContext<LMSMasterServiceDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnString")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ILoingService, LoginService>();
builder.Services.AddScoped<ILoginRepo, LoginRepo>();

builder.Services.AddScoped<IAdminMasterService, AdminMasterService>();
builder.Services.AddScoped<IAdminMasterRepo, AdminMasterRepo>();
builder.Services.AddScoped<IUserMasterService, UserMasterService>();
builder.Services.AddScoped<IUserMasterRepo, UserMasterRepo>();

//builder.Services.AddTransient<LoginDBTypes>();
builder.Services.AddTransient<EmailService>();
//builder.Services.AddTransient<CategoryTableDBTypes>();
//builder.Services.AddTransient<FamilyTableDBTypes>();
//builder.Services.AddTransient<ExpenseMasterTableDBTypes>();

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
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.UseSession();

app.Run();
