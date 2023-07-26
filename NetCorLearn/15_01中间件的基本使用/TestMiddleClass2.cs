namespace _15_01中间件的基本使用
{
    public class TestMiddleClass2
    {
        private readonly RequestDelegate next;

        public TestMiddleClass2(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            string password = httpContext.Request.Query["password"];  //https://localhost:7244/test?password=111

            if (password == "111")
            {
                if (httpContext.Request.HasJsonContentType())//是否json请求
                {
                    await httpContext.Response.WriteAsync("json <br/>");
                }
                await next.Invoke(httpContext);
            }
            else
            {
                httpContext.Response.StatusCode = 401;
            }

        }
    }

}
