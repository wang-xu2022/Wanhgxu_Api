//获取当前环境：提供两种方法
//方法一
//① var env = Environment.GetEnvironmentVariables();
//从env中获取环境
//② var envName = env["ASPNETCORE_ENVIRONMENT"];
//方法二
//var env2 = WebApplication.CreateBuilder().Environment.EnvironmentName;
//读取配置文件
//IConfiguration configuration = new ConfigurationBuilder().AddJsonFile($"appsettings.{env2}.json").Build();

//读取配置文件
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Waangxuapi.Core.DAL.Helper;
using Waangxuapi.Core.DAL.IHelper;
using wangxuapi.Core.Model;
using Wangxuapi.Core.Common.Helper;
using Wangxuapi.Core.Service.UserBll;
using Wanhgxu_Api;

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
var builder = WebApplication.CreateBuilder(args);

//读取配置文件
builder.Services.AddSingleton(new AppSettings(configuration));
var Connection = AppSettings.App(new string[] { "AppSettings", "ConnectionString" });
Console.WriteLine($"ConnectionString:{Connection}");
//Connection通过读取配置文件得到，其实就是数据库连接字符串，如何读取前面讲过
builder.Services.AddDbContext<EFDbContext>(options =>
options.UseSqlServer(Connection));
////读取Mysql配置文件
//var MySqlConnection = AppSettings.App(new string[] { "AppSettings", "MyConnectionString" });
////Connection通过读取配置文件得到，其实就是数据库连接字符串，如何读取前面讲过
//builder.Services.AddDbContext<CodeFirstContext>(options =>
//  options.UseMySql(MySqlConnection, new MySqlServerVersion(new Version(8, 0, 2))));

// Add services to the container.
//跨域
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ICityDAL, CityDAL>();
builder.Services.AddScoped<UserBll>();

//log4net
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddLog4Net("log4net.config");
});


//为Swagger配置JWT
builder.Services.AddSwaggerGen(option => {
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "后台接口",
    });
    var file = Path.Combine(AppContext.BaseDirectory, "Wanhgxu_Api.xml");  // xml⽂档绝对路径
    var path = Path.Combine(AppContext.BaseDirectory, file); // xml⽂档绝对路径
    option.IncludeXmlComments(path, true); // true : 显⽰控制器层注释
    option.OrderActionsBy(o => o.RelativePath); // 对action的名称进⾏排序，如果有多个，就可以看见效果了。

    // 在header中添加token，传递到后台
    //安装包 Swashbuckle.AspNetCore.Filters
    option.OperationFilter<SecurityRequirementsOperationFilter>();
    #region Token绑定到ConfigureServices
    option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
        Name = "Authorization",//jwt默认的参数名称
        In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
        Type = SecuritySchemeType.ApiKey
    });
    #endregion
});
//注册身份验证服务
builder.Services.AddAuthorizationSetup();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseRouting();
//身份验证
app.UseAuthentication();

app.UseAuthorization();
//注册中间件
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();
