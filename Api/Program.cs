using Api.Configs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ServiceConfig.AddServices(builder.Services, builder.Configuration);
ServiceConfig.AddDbContexts(builder.Services);
ServiceConfig.AddAuth(builder);
ServiceConfig.AddControllers(builder.Services);

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
