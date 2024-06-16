using Ocelot.DependencyInjection;
using Ocelot.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// ���������� ������������ � Endpoints API Explorer
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ���������� Ocelot � SwaggerForOcelot
builder.Services.AddOcelot();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

// �������� ������������ ocelot.json
builder.Configuration.AddJsonFile("ocelot.json");

WebApplication app = builder.Build();

// ��������� middleware
app.UseHttpsRedirection();
app.UseAuthorization();

// ��������� SwaggerForOcelot UI
app.UseSwaggerForOcelotUI(options =>
{
    options.PathToSwaggerGenerator = "/swagger/docs";
});

app.MapControllers();

// ������ Ocelot
await app.UseOcelot();
await app.RunAsync();
