namespace _15_01中间件的基本使用
{
    //基本中间件类
    public class TestMiddleClass
    {
        private readonly RequestDelegate next;

        public TestMiddleClass(RequestDelegate next)
        {
            this.next = next;
        }

        public  async Task InvokeAsync(HttpContext httpContext)
        {
            await httpContext.Response.WriteAsync("TestMiddleClass start <br/>");
            await next.Invoke(httpContext);
            await httpContext.Response.WriteAsync("TestMiddleClass end <br/>");
        }
    }
}
