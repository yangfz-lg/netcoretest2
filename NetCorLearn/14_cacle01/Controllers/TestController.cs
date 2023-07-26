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
        //内存缓存不会过器，除非重启服务器，
        //1、在数据改变时叼你个Remove或者Set 来删除或修改缓存
        //2、过期时间：觉得过期时间，滑动过期时间
        ////缓存穿透问题：传入ID不存在的数据会每次都查询数据库，解决方法：可把null 值也存入缓存 ，使用GetOrCreateAsync方法就可解决
        //缓存穿透雪崩：缓存项集中过期引起缓存雪崩 ，解决方法:加一个随机的过期时间
        [HttpGet]
        public async Task<ActionResult<Book?>>  GetBookById2(long Id)
        {

            Console.WriteLine("开始查询");
            Book? result = await memoryCache.GetOrCreateAsync("Book" + Id, async (e) => {
                Console.WriteLine("缓存中不存在,重新获取");

                //设置缓存过期时间
                e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20); //绝对过期时间，缓存有效期10秒
                e.SlidingExpiration = TimeSpan.FromSeconds(5);//滑动过期10秒

                //缓存穿透雪崩：缓存项集中过期引起缓存雪崩 ，解决方法:加一个随机的过期时间
                //e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(Random.Shared.Next(10, 15));

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
