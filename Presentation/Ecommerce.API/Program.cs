#region using
using Eccomerce.Infrastructure.Filters;
using Eccomerce.Infrastructure.ServiceRegistration;
using Ecomerce.Application.Validators.Products;
using Ecomerce.Infrastructure.Concreate.Storages.Azure;
using Ecommerce.Persistence.ServiceRegistrations;
using FluentValidation.AspNetCore;
#endregion

#region builder

var builder = WebApplication.CreateBuilder(args);

#region Services
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureService();
//Local Ve diger servisler
builder.Services.AddStorage<AzureStorage>();
//builder.Services.AddStorage<AzureStorage>();
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

#region file services 
//public async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
//{
//    string newFileName = await Task.Run<string>(async () =>
//    {
//        string extension = Path.GetExtension(fileName);
//        string newFileName = string.Empty;
//        if (first)
//        {
//            string oldName = Path.GetFileNameWithoutExtension(fileName);
//            newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
//        }
//        else
//        {
//            newFileName = fileName;
//            int indexNo1 = newFileName.IndexOf("-");
//            if (indexNo1 == -1)
//                newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
//            else
//            {
//                int lastIndex = 0;
//                while (true)
//                {
//                    lastIndex = indexNo1;
//                    indexNo1 = newFileName.IndexOf("-", indexNo1 + 1);
//                    if (indexNo1 == -1)
//                    {
//                        indexNo1 = lastIndex;
//                        break;
//                    }
//                }

//                int indexNo2 = newFileName.IndexOf(".");
//                string fileNo = newFileName.Substring(indexNo1 + 1, indexNo2 - indexNo1 - 1);

//                if (int.TryParse(fileNo, out int _fileNo))
//                {
//                    _fileNo++;
//                    newFileName = newFileName.Remove(indexNo1 + 1, indexNo2 - indexNo1 - 1)
//                        .Insert(indexNo1 + 1, _fileNo.ToString());
//                }
//                else
//                    newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
//            }
//        }

//        if (File.Exists($"{path}\\{newFileName}"))
//            return await FileRenameAsync(path, newFileName, false);
//        else
//            return newFileName;
//    });

//    return newFileName;
//}
#endregion