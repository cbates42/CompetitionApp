using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private Service _service = new Service();

      [HttpGet]
      [Route("get-all")]
      public IEnumerable<RecordModel> Get()
        {

            return _service.APIGetAll();
        }

        [HttpGet]
        [Route("get-by-Id/{id}")]
        public RecordModel Get(int id)
        {
            return _service.APIGetById(id);
        }

        [HttpGet]
        [Route("get-by-name/{name}")]
        public IEnumerable<RecordModel> Get(string name)
        {
            return _service.APIGetbyName(name);
        }

        [HttpPost]
        [Route("insert-record")]
        public int Post([FromBody] RecordModel model)
        {
            return _service.APIInsertRecord(model);
        }

        [HttpDelete]
        [Route("delete-by-id/{id}")]
        public string Delete(int id)
        {
            return _service.APIDeleteById(id);
        }
    }
}
