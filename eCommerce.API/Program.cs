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
.AddPolicyHandler(builder.Services.BuildServiceProvider().GetRequiredService<IUsersMicroservicePolicies>()
.GetUsersMicroserviceRetryPolicy())

.AddPolicyHandler(builder.Services.BuildServiceProvider().GetRequiredService<IUsersMicroservicePolicies>().GetUsersMicroserviceTimeOutPolicy());

builder.Services.AddHttpClient<ProductsMicroserviceHttpClient>(client =>
{
    client.BaseAddress = new Uri($"http://localhost:{Environment.GetEnvironmentVariable("PRODUCTS_MICROSERVICE_PORT")}/api/products/");
})

.AddPolicyHandler(builder.Services.BuildServiceProvider().GetRequiredService<IProductsMicroservicePolicies>().GetProductsMicroserviceFallbackPolicy())

.AddPolicyHandler(builder.Services.BuildServiceProvider().GetRequiredService<IProductsMicroservicePolicies>().GetProductsMicroserviceTimeoutPolicy());

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
