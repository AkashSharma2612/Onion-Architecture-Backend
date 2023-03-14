
using Domain.Dto;
using Domain.FluentValidator;
using Domain.Models;
using Domain.ViewModel;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence;
using ServiceLayer.Repository;
using ServiceLayer.Repository.IRepository;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Utility;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITaskUsersRepository, TaskUsersRepository>();
builder.Services.AddScoped<ITeamProjectsRepository, TeamProjectRepository>();
builder.Services.AddScoped<ITeamUsersRepository, TeamUsersRepository>();
builder.Services.AddScoped<IUTaskRepository, UTaskRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITaskInformationDtoRepository, TaskInforamationDtoRepository>();

//fulent validator class added
builder.Services.AddScoped<IValidator<ApplicationUserDto>,ApplicatioUserDtoValidator>();
builder.Services.AddScoped<IValidator<LoginViewModel>, LoginViewModelValidator>();
// Add services to the container.
builder.Services.AddControllers()
                .AddFluentValidation(options =>
                {
                    // Validate child properties and root collection elements
                    options.ImplicitlyValidateChildProperties = true;
                    options.ImplicitlyValidateRootCollectionElements = true;

                    // Automatic registration of validators in assembly
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "myPolicy", Builder =>

    {
        Builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});

// persistence
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();
/*Here we will create the role by code */
/*IServiceScopeFactory serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (IServiceScope scope = serviceScopeFactory.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    if (!await roleManager.RoleExistsAsync(SD.Role_Admin))
    {
        var role = new IdentityRole();
        role.Name = SD.Role_Admin;
        await roleManager.CreateAsync(role);
    }
    if (!await roleManager.RoleExistsAsync(SD.Role_Employee))
    {
        var role = new IdentityRole();
        role.Name = SD.Role_Employee;
        await roleManager.CreateAsync(role);
    }
}*/
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors("myPolicy");
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();

