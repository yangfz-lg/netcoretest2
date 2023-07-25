using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection.Metadata.Ecma335;

namespace _14_cacle01.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;  //内存缓存
        private readonly ILogger<TestController> logger;

        public TestController(IMemoryCache memoryCache, ILogger<TestController> logger)
        {
            this.memoryCache = memoryCache;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<Book?>  GetBookById(long Id)
        {
            Book? result = BookService.GetBookById(Id);
            if (result == null)
            {
                return NotFound("未找到");
            }
            else
            {
                return result;
            }

        }

        //使用内存缓存
        [HttpGet]
        public async Task<ActionResult<Book?>>  GetBookById2(long Id)
        {

            Console.WriteLine("开始查询");
            Book? result = await memoryCache.GetOrCreateAsync("Book" + Id, async (e) => {
                Console.WriteLine("缓存中不存在,重新获取");
                return await BookService.GetBookByIdAsync(Id); });
            if (result == null)
            {
                return NotFound("未找到");
            }
            else
            {
                return result;
            }
        }


        //客户端响应式缓存
        [ResponseCache(Duration =30)]// 自动增加 cache-control报文头：cache-control: public,max-age=30   ，缓存存在30秒
        [HttpGet]
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
