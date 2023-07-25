var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//注册内存缓存
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//服务器缓存 app.MapControllers() 之前加上app.UseResponseCaching()
//确保app.UseCors() 写在app.UseResponseCaching()之前
//客户端请求头中添加cache-control:no-cache 客户端跟服务器端缓存都会禁用 
//服务器缓存比较鸡肋
//app.UseResponseCaching(); //启用服务器端缓存


app.MapControllers();

app.Run();
