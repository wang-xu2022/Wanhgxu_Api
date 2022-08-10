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
//Connection通过读取配置文件得到，其实就是数据库连接字符串。
builder.Services.AddDbContext<EFDbContext>(options =>
options.UseSqlServer(Connection));

////读取Mysql配置文件
//var MySqlConnection = AppSettings.App(new string[] { "AppSettings", "MyConnectionString" });
////Connection通过读取配置文件得到，其实就是数据库连接字符串。
//builder.Services.AddDbContext<CodeFirstContext>(options =>
//  options.UseMySql(MySqlConnection, new MySqlServerVersion(new Version(8, 0, 2))));

// Add services to the container.


//跨域HTTP请求
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

//log4net配置错误日志
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
    //配置Swagger中间件
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");//捕获以下中间件中引发的异常
    app.UseHsts();//HTTP 严格传输安全协议 (HSTS) 中间件
}

//app.UseHttpsRedirection();
app.UseRouting();
//身份验证中间件
app.UseAuthentication();
//注册中间件UseCors、UseAuthentication 和 UseAuthorization 必须按显示的顺序出现
app.UseCors();
//用于授权用户访问安全资源的授权中间件 
app.UseAuthorization();
app.MapControllers();
app.Run();
