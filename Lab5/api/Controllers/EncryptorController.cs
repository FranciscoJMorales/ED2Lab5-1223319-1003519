using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncryptorController : ControllerBase
    {
        readonly IWebHostEnvironment env;

        public EncryptorController(IWebHostEnvironment _env)
        {
            env = _env;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return null;
        }

        [HttpPost]
        [Route("/api/cipher/{method}")]
        public IActionResult Cipher([FromForm] IFormFile file, [FromForm] string key, string method)
        {
            return Ok();
        }

        [HttpPost]
        [Route("/api/decipher")]
        public IActionResult Decipher([FromForm] IFormFile file, [FromForm] string key)
        {
            return Ok();
        }
    }
}
