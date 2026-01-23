using bidPursuit.API.Exceptions;
using bidPursuit.API.Extensions;
using bidPursuit.Application.Extensions;
using bidPursuit.Infrastructure.Extensions;
using bidPursuit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<ErrorHandlingMiddleware>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddInfrastructure(builder.Configuration).AddApplication(builder.Configuration);
builder.AddExtras();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BidPursuitDbContext>();
    db.Database.Migrate();
}

app.UseMiddleware<ErrorHandlingMiddleware>();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
