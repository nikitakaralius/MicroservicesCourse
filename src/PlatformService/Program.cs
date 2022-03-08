// ReSharper disable SuggestVarOrType_SimpleTypes

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Environment, builder.Configuration);

var app = builder.Build();
Console.WriteLine($"==>Using CommandService Endpoint: {app.Configuration.CommandsService()}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.PrepareDbPopulation(builder.Environment);
app.Run();
