using _15_01中间件的基本使用;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//简单的中间件，根据请求路径返回信息
//app.MapGet("/", () => "Hello World!");
//app.MapGet("/test", () => "test");


app.Map("/test", async (pipeBuilder) => {
     pipeBuilder.Use(async (context, next) =>
    {
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync("1 <br/>");
        await next.Invoke(); //执行下一个use
        await context.Response.WriteAsync("1.1 <br/>");  //最后执行
    });
    pipeBuilder.Use(async (context, next) =>
    {
        await context.Response.WriteAsync("2 <br/>");
        await next.Invoke();//执行下一个use
        await context.Response.WriteAsync("2.1 <br/>");
    });

    //调用中间件类
    pipeBuilder.UseMiddleware<TestMiddleClass>();

    //调用中间件类
    pipeBuilder.UseMiddleware<TestMiddleClass2>();

    pipeBuilder.Run(async (context) =>
    {
        await context.Response.WriteAsync("run <br/>");     
    });

});



app.Run();
