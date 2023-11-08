#region using
using Eccomerce.Infrastructure.Filters;
using Eccomerce.Infrastructure.ServiceRegistration;
using Ecomerce.Application.Validators.Products;
using Ecommerce.Persistence.ServiceRegistrations;
using FluentValidation.AspNetCore;
#endregion

#region builder

var builder = WebApplication.CreateBuilder(args);

#region Services
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureService();
#endregion

//asp.net corun validatiasi OFF
//flent validation ON
#region Vlidation
builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()).AddFluentValidation(
configuration => { configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>(); })
.ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });
#endregion

#region CorsPolicy
builder.Services
.AddCors(options => options.
AddDefaultPolicy(policy => policy
.AllowAnyHeader()
.AllowAnyMethod()
.AllowAnyOrigin()
));
#endregion


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region app
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion

