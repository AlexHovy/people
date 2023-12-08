using Api.Configs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
StartupConfig.AddServices(builder.Services, builder.Configuration);
StartupConfig.AddDbContexts(builder.Services);
StartupConfig.AddAuth(builder);
StartupConfig.AddControllers(builder.Services);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

StartupConfig.AddDbSeedData(app);

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
