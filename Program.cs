using QR_code_Api.Models;
using QR_code_project.Models;
//using QR_code_Api.Services;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddDbContext<BusinessCardQrCode>(options =>
//{
//    options.UseSqlServer(Configuration["BusinessCardQrcode"]);
//});
// Add services to the container.
builder.Services.Configure<QRCodeDatabaseSettings>(
    builder.Configuration.GetSection("BusinessCardQrcode"));

builder.Services.AddSingleton<BusinessService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

