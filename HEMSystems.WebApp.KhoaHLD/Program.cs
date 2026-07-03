using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;
using HEMSystems.Entities.KhoaHLD.Models;
using HEMSystems.Repositories.KhoaHLD;
using HEMSystems.Services.KhoaHLD;
using HEMSystems.WebApp.KhoaHLD.Commons;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

static IEdmModel GetEdmModel()
{
    var odataBuilder = new ODataConventionModelBuilder();
    odataBuilder.EntitySet<ProjectSubmissionsKhoaHld>("ProjectSubmissionsKhoaHld")
        .EntityType
        .HasKey(projectSubmission => projectSubmission.SubmissionKhoahldId);

    return odataBuilder.GetEdmModel();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddOData(options => options
        .Select()
        .Filter()
        .OrderBy()
        .Expand()
        .Count()
        .SetMaxTop(null)
        .AddRouteComponents("odata", GetEdmModel()))
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ProjectSubmissionsKhoaHldRepository>();
builder.Services.AddScoped<IProjectSubmissionsKhoaHldService, ProjectSubmissionsKhoaHldService>();
builder.Services.AddScoped<SystemUserAccountRepository>();
builder.Services.AddScoped<ISystemUserAccountService, SystemUserAccountService>();
builder.Services.AddSingleton<ITokenBlacklistService, TokenBlacklistService>();
builder.Services.AddAuthorization();
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty)),
            // ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                var jti = context.Principal?.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
                var blacklist = context.HttpContext.RequestServices.GetRequiredService<ITokenBlacklistService>();

                if (jti != null && blacklist.IsRevoked(jti))
                {
                    context.Fail("This token has been revoked.");
                }

                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddSwaggerGen(option =>
{
    option.DescribeAllParametersInCamelCase();
    option.ResolveConflictingActions(conf => conf.First());
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
    option.DocumentFilter<HideODataEntityPathsDocumentFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

public sealed class HideODataEntityPathsDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var pathsToHide = swaggerDoc.Paths.Keys
            .Where(path =>
                path.StartsWith("/odata/", StringComparison.OrdinalIgnoreCase)
                && !path.Equals("/odata/$metadata", StringComparison.OrdinalIgnoreCase))
            .ToList();

        foreach (var path in pathsToHide)
        {
            swaggerDoc.Paths.Remove(path);
        }
    }
}
