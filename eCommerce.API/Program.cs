using eCommerce.API.Middlewares;
using eCommerce.Core;
using eCommerce.Core.HttpClients;
using eCommerce.Core.Policies;
using eCommerce.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddTransient<IPollyPolicies, PollyPolicies>();

builder.Services.AddHttpClient<UsersMicroserviceHttpClient>(client =>
{
    string host = Environment.GetEnvironmentVariable("USERS_MICROSERVICE_HOST")!;
    string port = Environment.GetEnvironmentVariable("USERS_MICROSERVICE_PORT")!;

    client.BaseAddress = new Uri($"http://{host}:{port}/gateway/authentication/");
})
.AddPolicyHandler((services, request) =>
{
    return services.GetRequiredService<IPollyPolicies>().GetCombinedPolicyAsync();
});

builder.Services.AddHttpClient<ProductsMicroserviceHttpClient>(client =>
{
    string host = Environment.GetEnvironmentVariable("PRODUCTS_MICROSERVICE_HOST")!;
    string port = Environment.GetEnvironmentVariable("PRODUCTS_MICROSERVICE_PORT")!;

    client.BaseAddress = new Uri($"http://{host}:{port}/gateway/products/");
})
.AddPolicyHandler((services, request) =>
{
    return services.GetRequiredService<IPollyPolicies>().GetCombinedPolicyAsync();
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionHandlingMiddleware();
app.UseStaticFiles();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseHttpsRedirection(); 
}
app.MapControllers();

app.Run();
