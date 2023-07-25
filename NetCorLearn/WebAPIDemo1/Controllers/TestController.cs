using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIDemo1.Controllers
{
    [Route("api/[controller]/[action]")] // api/Test/GetPerson
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public  entity.Person GetPerson()
        {
            return new entity.Person("eb青年", 18); 
        }

        [HttpGet]
        public entity.Person GetPersonByID(int id)
        {
            if (id == 1)
            {
                return new entity.Person("eb青年1号", 18);
            }
            if (id == 2)
            {
                return new entity.Person("eb青年2号", 18);
            }
            if (id == 3)
            {
                return new entity.Person("eb青年3号", 18);
            }
            return null;
        }

        [HttpPost]
        public string SaveNode(entity.SaveNoteRequest req)
        {
            System.IO.File.WriteAllText(req.title + ".txt" , req.str);
            return "完成";
        }

        //异步方法
        [HttpGet]
        public async Task<string> GetPerson2()
        {
            string s = await System.IO.File.ReadAllTextAsync("d:/temp/1.txt");
            return s;
        }

        [HttpGet]
        public IActionResult GetPersonByID2(int id)
        {
            if (id == 1)
            {
                return Ok(201);
            }
            if (id == 2)
            {
                return Ok(202);
            }
            if (id == 3)
            {
                return Ok(new entity.Person("eb青年3号", 18));
            }
            return NotFound("错误");  //NotFound 返回404错误
        }

        [HttpGet]
        public ActionResult<int> GetPersonByID3(int id)
        {
            if (id == 1)
            {
                return 201;
            }
            if (id == 2)
            {
                return 202;
            }
            if (id == 3)
            {
                return 203;
            }
            return 444;  //NotFound 返回404错误
        }

        [HttpGet("{i1}/{i2}")]  //url里面添加定位参数  /api/Test/multi/i1/i2
        public int multi(int i1,int i2)
        {
            return i1 + i2;
        }
    }
}
