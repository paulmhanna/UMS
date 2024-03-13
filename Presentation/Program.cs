using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.OData;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Persistence.Entities;
using Presentation;

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<Course>("Courses");
    builder.EntitySet<TeacherPerCourse>("TeacherPerCourses");
    builder.EntitySet<ClassEnrollment>("ClassEnrollment");
    return builder.GetEdmModel();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddOData(options => options
    .AddRouteComponents("odata", GetEdmModel())
    .Select()
    .Filter()
    .OrderBy()
    .SetMaxTop(20)
    .Count()
    .Expand()
).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
builder.Services.AddMvcCore(options =>

{
    foreach (var outputFormatter in
             options.OutputFormatters.OfType<OutputFormatter>().Where(x =>
                 x.SupportedMediaTypes.Count == 0))
        outputFormatter.SupportedMediaTypes.Add(
            new MediaTypeHeaderValue(
                "application/prs.odatatestxx-odata"));

    foreach (var inputFormatter in
             options.InputFormatters.OfType<InputFormatter>().Where(
                 x => x.SupportedMediaTypes.Count == 0))
        inputFormatter.SupportedMediaTypes.Add(
            new MediaTypeHeaderValue(
                "application/prs.odatatestxx-odata"));
});
builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/lab2-ums";
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://securetoken.google.com/lab2-ums",
            ValidateAudience = true,
            ValidAudience = "lab2-ums",
            ValidateLifetime = true
        };
    });
builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig
{
    ApiKey = "AIzaSyDzDh5GCqtNHr_6RgnOz8KH50lnsonj0eo",
    AuthDomain = "lab2-ums.firebaseapp.com",
    Providers = new FirebaseAuthProvider[]
    {
        new EmailProvider()
    }
}));
var apiVersioningSettings = builder.Configuration.GetSection("ApiVersioning");
builder.Services.Configure<ApiVersioningOptions>(apiVersioningSettings);
builder.Services.ConfigureServices();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "UMS", Version = "v1" }); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();