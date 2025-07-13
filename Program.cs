
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Messenger.Application.Services;
using Messenger.Infrastracture.Database;
using Messenger.Infrastracture.Repositories;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;



var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MessengerContext>(options =>
{
    options.UseSqlServer(connectionString);
});


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserBlocksRepository, UserBlocksRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IIncomingService, IncomingService>();
builder.Services.AddScoped<IOutgoingService, OutgoingService>();
builder.Services.AddScoped<IOutgoingRepository, OutgoingRepository>();
builder.Services.AddScoped<IIncomingRepository, IncomingRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Messenger API", Version = "v1" });
    c.EnableAnnotations();
});


// builder.Services.AddControllers();




// var collector = new ProviderCollector();
//
// using (var provider = new DatabaseProvider(collector))
// {
//     var tablesCount = provider.GetTablesCount();
//     Console.WriteLine(tablesCount);
// }
//
// Console.WriteLine("Press any key to exit...");;





var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MessengerContext>();
    // dbContext.Database.EnsureDeleted();
    // dbContext.Database.EnsureCreated();
    
    
}



    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Messenger API v1");
            c.RoutePrefix = string.Empty;
            c.SupportedSubmitMethods(Array.Empty<SubmitMethod>());
        });
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();












