using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DEMO1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        //通过构造函数注入TestService 服务
        private readonly TestService testService;
        

        public TestController(TestService testService)
        {
            this.testService= testService;
            
        }

        [HttpGet]
        public  int GetSum(int x,int y)
        {
            //注入服务后可使用TestService里Add方法
            return testService.Add(x, y);
        }

        //特色情况，通过 FromServices 注入只要调用GetSum2服务才会注入TestService2 ，不影响GetSum 速度
        [HttpGet]
        public int GetSum2([FromServices] TestService2 testService2, int y)
        {
            //注入服务后可使用TestService里Add方法
            return testService.Add(testService2.num, y);
        }

        
    }
}
