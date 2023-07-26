using _15_01�м���Ļ���ʹ��;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//�򵥵��м������������·��������Ϣ
//app.MapGet("/", () => "Hello World!");
//app.MapGet("/test", () => "test");


app.Map("/test", async (pipeBuilder) => {
     pipeBuilder.Use(async (context, next) =>
    {
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync("1 <br/>");
        await next.Invoke(); //ִ����һ��use
        await context.Response.WriteAsync("1.1 <br/>");  //���ִ��
    });
    pipeBuilder.Use(async (context, next) =>
    {
        await context.Response.WriteAsync("2 <br/>");
        await next.Invoke();//ִ����һ��use
        await context.Response.WriteAsync("2.1 <br/>");
    });

    //�����м����
    pipeBuilder.UseMiddleware<TestMiddleClass>();

    //�����м����
    pipeBuilder.UseMiddleware<TestMiddleClass2>();

    pipeBuilder.Run(async (context) =>
    {
        await context.Response.WriteAsync("run <br/>");     
    });

});



app.Run();
