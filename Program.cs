using Messenger.Api.Repositories;
using Messenger.Infrastractures.Database;
using Messenger.Repositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FluentValidation;
using Messenger.Mappers;
using Messenger.Models;
using Messenger.Repositories.MessageRepositories;
using Messenger.Services;
using Messenger.Services.MessageServices;
using Messenger.Validations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MessengerContext>(options =>
{
    options.UseSqlServer(connectionString);
});


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IIncomingMessageService, IncomingMessageService>();
builder.Services.AddScoped<IOutcomingMessageService, OutcomingMessageService>();
builder.Services.AddScoped<IOutcomingMessageRepository, OutcomingMessageRepository>();
builder.Services.AddScoped<IIncomingMessageRepository, IncomingMessageRepository>();


builder.Services.AddAutoMapper(typeof(UserMapper)); 



builder.Services.AddValidatorsFromAssemblyContaining<UserValidation>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Messenger API", Version = "v1" });
});


builder.Services.AddControllers();




var collector = new ProviderCollector();

using (var provider = new DatabaseProvider(collector))
{
    var tablesCount = provider.GetTablesCount();
    Console.WriteLine(tablesCount);
}

Console.WriteLine("Press any key to exit...");;





var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MessengerContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Messenger API v1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection(); 
app.UseAuthorization();
app.MapControllers();
app.Run();











