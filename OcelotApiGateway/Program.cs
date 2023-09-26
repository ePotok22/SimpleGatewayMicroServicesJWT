using SharedApi;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

const string ocelotConfig = "ocelot.json";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile(ocelotConfig, false, false);

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddJwtAuthentication();

WebApplication app = builder.Build();

await app.UseOcelot();

app.UseAuthentication();
app.UseAuthorization();

app.Run();