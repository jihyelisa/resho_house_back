using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// DB 연결 문자열 가져오기
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// DbContext 등록
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
// builder.Services.AddAuthorization();
// builder.LogTo(Console.WriteLine, LogLevel.Information);

// 서비스 등록
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Migration을 적용하고, 데이터베이스를 업데이트한 후 데이터 삽입
// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     dbContext.Database.Migrate(); // DB 업데이트
//     // 데이터 삽입은 OnModelCreating에서 처리되므로 추가로 호출할 필요는 없습니다.
// }

// 간단한 루트 설정
//app.MapGet("/", () => "Hello World!");

app.UseRouting();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();