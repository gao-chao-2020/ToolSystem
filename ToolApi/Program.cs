using FreeSql;
using ToolApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IIpService, IpService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string conn = "server=192.168.1.108;database=IPDB;uid=sa;pwd=1234;MultipleActiveResultSets=true";
// FreeSql 
var freeSqlBuilder = new FreeSqlBuilder()
        .UseConnectionString(DataType.SqlServer, conn)
        .UseAutoSyncStructure(true)
        .UseNoneCommandParameter(true);

var fsql = freeSqlBuilder.Build();

builder.Services.AddSingleton(fsql);
builder.Services.AddFreeRepository();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
