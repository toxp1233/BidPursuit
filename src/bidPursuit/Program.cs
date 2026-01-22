using bidPursuit.API.Exceptions;
using bidPursuit.API.Extensions;
using bidPursuit.Application.Extensions;
using bidPursuit.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<ErrorHandlingMiddleware>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddInfrastructure(builder.Configuration).AddApplication(builder.Configuration);
builder.AddExtras();

var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
