// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;
using testMvc.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// var tkConfig = builder.Configuration.GetSection("Jwt");
// var tokenValidationParameters = new TokenValidationParameters
// {
//     ValidateIssuer = true,
//     ValidateAudience = true,
//     ValidateLifetime = true,
//     ValidateIssuerSigningKey = true,
//     ValidIssuer = tkConfig["Issuer"],
//     ValidAudience = tkConfig["Audience"],
//     IssuerSigningKey = new SymmetricSecurityKey(
//         System.Text.Encoding.UTF8.GetBytes(tkConfig["Key"]))
// };
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//   .AddJwtBearer(options =>
//   {
//       options.TokenValidationParameters = tokenValidationParameters;
//   });

builder.Services.AddControllersWithViews();
var db = new ApplicationDbContext();
db.Database.EnsureCreated();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "AuthCookie";
        options.Cookie.HttpOnly = true;
        options.LoginPath = "/user/Login";
    });
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
