using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore.Query.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _service;
        private string MimeType { get; set; }
        private string FileName { get; set; }

        public FilesController(IFileService service)
        {
            _service = service;
            MimeType = "";
            FileName = "";
        } 
        // GET: api/Files
        [HttpGet]
        public Task<IEnumerable<string>> Get()
        {
            return Task.FromResult(_service.FilesArray);
        }

        // GET api/Files/5
        [HttpGet("{id}")]
        public IActionResult GetFile(string id)
        {
            Stream file = _service.GetFile(id);
            MimeType = _service.MimeType;
            FileName = _service.FileName;
            return File(file, MimeType, FileName);
        }

        // POST api/<ImagesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ImagesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ImagesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
