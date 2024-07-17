using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VoucherCreation.BAL.IRepositories;
using VoucherCreation.BAL.Repositories;
using VoucherCreation.DAL;
using VoucherCreation.BAL.Infrastructure.AppSettingsConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddResponseCaching();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.WithOrigins(new string[] {
        "http://localhost:4200","http://localhost:4400"
    }).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
});

var AppSettingConfig = builder.Configuration.GetSection("AppSettings");

var JwtSecret = AppSettingConfig.GetValue<string>("JwtSecret");
var JwtIssuer = AppSettingConfig.GetValue<string>("JwtIssuer");

var scrt = System.Text.Encoding.ASCII.GetBytes(JwtSecret);

builder.Services.AddAuthentication(da =>
{
    da.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    da.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(da =>
{
    da.RequireHttpsMetadata = false;
    da.SaveToken = true;
    da.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidIssuer = JwtIssuer,
        ValidateIssuer = true,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(scrt),
        ValidateIssuerSigningKey = true,
        RequireExpirationTime = true,
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateActor = false,
        ValidateTokenReplay = false,
        RequireSignedTokens = false
    };
});


//Fetching Connection string from APPSETTINGS.JSON  
var ConnectionString = builder.Configuration.GetConnectionString("dbConnection");

//Entity Framework  
builder.Services.AddDbContext<PiVMSContext>(options => options.UseSqlServer(ConnectionString));

// Add services to the container.
builder.Services.AddMvc();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "pi_vms_creation", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

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
                     new string[] {}
             }
         });
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<AppSettings>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();


{
    var tmpPth = Path.Combine(Path.GetTempPath(), "pi_vms_creation_log.txt");
    File.WriteAllText(tmpPth, "LogPath:" + AppContext.BaseDirectory + "Logs");
}

if (Directory.Exists(AppContext.BaseDirectory + "Logs") == false)
    Directory.CreateDirectory(AppContext.BaseDirectory + "Logs");

builder.Services.AddLogging(lb =>
{
    lb.AddConfiguration(builder.Configuration.GetSection("Logging"));
    lb.AddFile(o => o.RootPath = AppContext.BaseDirectory);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
