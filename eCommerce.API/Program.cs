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
builder.Services.AddTransient<IUsersMicroservicePolicies, UsersMicroservicePolicies>();
builder.Services.AddTransient<IProductsMicroservicePolicies, ProductsMicroservicePolicies>();
builder.Services.AddHttpClient<UsersMicroserviceHttpClient>(client =>
{
    client.BaseAddress = new Uri($"http://localhost:{Environment.GetEnvironmentVariable("USERS_MICROSERVICE_PORT")}/api/authentication/");
})
.AddPolicyHandler((services, request) =>
{
    return services.GetRequiredService<IUsersMicroservicePolicies>().GetUsersMicroserviceCircuitBrakerPolicy();
})

.AddPolicyHandler((services, response) =>
{
    return services.GetRequiredService<IUsersMicroservicePolicies>().GetUsersMicroserviceTimeOutPolicy();
});

builder.Services.AddHttpClient<ProductsMicroserviceHttpClient>(client =>
{
    client.BaseAddress = new Uri($"http://localhost:{Environment.GetEnvironmentVariable("PRODUCTS_MICROSERVICE_PORT")}/api/products/");
})

.AddPolicyHandler((services, request) => 
{
    return services.GetRequiredService<IProductsMicroservicePolicies>().GetProductsMicroserviceFallbackPolicy();
})

.AddPolicyHandler((services, response) => 
{ 
    return services.GetRequiredService<IProductsMicroservicePolicies>().GetProductsMicroserviceTimeoutPolicy(); 
})

.AddPolicyHandler((services, response) =>
 {
     return services.GetRequiredService<IProductsMicroservicePolicies>().GetProductsMicroserviceBulkheadIsolationPolicy();
 });

var app = builder.Build(); 
app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionHandlingMiddleware();
app.UseStaticFiles();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseHsts();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
