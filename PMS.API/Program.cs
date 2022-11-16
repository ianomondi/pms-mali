using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PMS.API.Authorization;
using PMS.DAL;
using PMS.DAL.IRepos;
using PMS.DAL.Repos;
using PMS.Domain.IServices;
using PMS.Domain.Models;
using PMS.Services.Contracts;
//using PMS.Domain.Services;
using PMS.Services.DomainServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<PMSDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), m => m.MigrationsAssembly("PMS.DAL")));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddScoped<IRolesRepo, RolesRepo>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<ICompanyServices, CompanyService>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();

builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IUserAuthService, UserAuthService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IAuthorizationHandler, RoleHasPermissionHandler>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

//Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PolicyMapper.CAN_CREATE_COMPANY, policy =>
        policy.RequireAssertion(context => context.User.HasClaim(c =>
            (c.Type == CustomClaimTypes.PERMISSION_CAN_CREATE_COMPANY)) 
                || context.User.IsInRole(RoleMapper.SUPER_ADMIN)
                || context.User.IsInRole(RoleMapper.AGENCY)
                || context.User.IsInRole(RoleMapper.LANDLORD)
            ));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PolicyMapper.CAN_DELETE_COMPANY, policy =>
        policy.RequireAssertion(context => context.User.HasClaim(c =>
            (c.Type == CustomClaimTypes.PERMISSION_CAN_DELETE_COMPANY)) 
                || context.User.IsInRole(RoleMapper.SUPER_ADMIN)
                || context.User.IsInRole(RoleMapper.AGENCY)
                || context.User.IsInRole(RoleMapper.LANDLORD)
            ));
});

//check if role has certain permission
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PolicyMapper.CAN_ROLE_ADD_USER, policy =>
        policy.Requirements.Add(new RoleHasPermissionRequirement(RoleMapper.AGENCY, RolePermissionsMapper.PERMISSION_CAN_ADD_USER)));
});
//End Policies

var app = builder.Build();
app.UseAuthentication();

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
