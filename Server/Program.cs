using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Server.API;
using Server.Generic_Repository;
using Server.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


// Add services to the container
builder.Services.AddDbContext<CollegeDataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MinimalAPI")));
builder.Services.AddScoped(typeof(IGenericRepository), typeof(GenericRepository));


// Add Cors
builder.Services.AddCors(setupAction: options =>
{
    options.AddDefaultPolicy(configurePolicy: builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();

    });
});


// Adding Authentication  
/*builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})*/

// Adding Jwt Bearer  
/*.AddJwtBearer(options =>
{
    options.SaveToken = true;
    //options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});*/


//builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Enter 'Bearer {token}' to authorize"
    });

    //authorization header for all endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});*/

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    /*app.UseSwaggerUI(options =>
    {
        options.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Example);
        options.DefaultModelExpandDepth(0);
        options.DefaultModelsExpandDepth(-1);
        options.EnableDeepLinking();
        options.DisplayOperationId();
        options.DisplayRequestDuration();
        options.ShowExtensions();

        options.ConfigObject.AdditionalItems["token"] = "Bearer ";
        options.ConfigObject.AdditionalItems["AuthorizationKeyName"] = "Authorization";
    });*/
}
app.UseCors();
app.UseHttpsRedirection();


//app.UseAuthentication();
//app.UseAuthorization();

// Map all the API End points
app.MapMethodsUsingGenericRepositoryRoutes();


app.Run();