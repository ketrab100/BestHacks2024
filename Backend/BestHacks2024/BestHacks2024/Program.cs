using BestHacks2024.Database.Entities;
using BestHacks2024.Database.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using BestHacks2024.Database;
using BestHacks2024.Interfaces;
using BestHacks2024.Mappings;
using BestHacks2024.Services;

namespace BestHacks2024
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173");
                    });
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BestHacksApi",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            }, new string[] {} }});
            });

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            
            builder.Services.AddDbContext<BestHacksDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("SqlConnection"));
            });
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IEmployerService, EmployerService>();
            builder.Services.AddScoped<IMatchService, MatchService>();
            builder.Services.AddScoped<ITagService, TagService>();

            builder.Services.AddIdentity<User, IdentityRole<Guid>>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 1;
            }).AddEntityFrameworkStores<BestHacksDbContext>();

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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            
            
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var dbcontext = scope.ServiceProvider.GetRequiredService<BestHacksDbContext>();
            dbcontext.Database.Migrate();

            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    .AllowCredentials() // allow credentials
            );
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            if (bool.Parse(builder.Configuration["ShouldGenerateMockData"]))
            {
                using (var mockScope = app.Services.CreateScope())
                {
                    var dbContext = mockScope.ServiceProvider.GetRequiredService<BestHacksDbContext>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    if (!dbContext.Employees.Any() && !dbContext.Employers.Any() && !dbContext.Matches.Any())
                    {
                        var tags = StaticMockDataGenerator.GenerateTags(15); // Generate 15 unique tags
                        var employees = StaticMockDataGenerator.GenerateEmployees(10, tags);
                        var employers = StaticMockDataGenerator.GenerateEmployers(10, tags);

                        foreach (var employee in employees)
                        {
                            var password = "Employee123!"; 
                            var result = userManager.CreateAsync(employee, password).Result;
                            if (!result.Succeeded)
                            {
                                throw new Exception($"Failed to create employee user: {result.Errors.FirstOrDefault()?.Description}");
                            }
                        }

                        foreach (var employer in employers)
                        {
                            var password = "Employer123!"; 
                            var result = userManager.CreateAsync(employer, password).Result;
                            if (!result.Succeeded)
                            {
                                throw new Exception($"Failed to create employer user: {result.Errors.FirstOrDefault()?.Description}");
                            }
                        }
                        //
                        //var matches = StaticMockDataGenerator.GenerateMatches(employees, employers);
                        //dbContext.Matches.AddRange(matches);
                        dbContext.SaveChanges();
                    }
                }
            }

            app.Run();
        }
    }
}
