using BudgetBitesAPI.InfraStructure.ServiceExtension;
using BudgetBitesAPI.Services.Interfaces;
using BudgetBitesAPI.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var allowSpecificOrigins = "Angular Sites";

// Add services to the container.
builder.Services.AddDIServices(builder.Configuration);
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserDetailsService, UserDetailService>();
builder.Services.AddScoped<IPreferenceService, PreferenceService>();
builder.Services.AddScoped<IFavouriteService, FavouriteService>();
builder.Services.AddScoped<IBudgetService, BudgetService>();
builder.Services.AddScoped<IMealPlanService, MealPlanService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(allowSpecificOrigins,
                              policy =>
                              {
                                  policy.WithOrigins("http://localhost:4200")
                                                      .AllowAnyHeader()
                                                      .AllowAnyMethod();
                              });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? ""))
            };
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(allowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
