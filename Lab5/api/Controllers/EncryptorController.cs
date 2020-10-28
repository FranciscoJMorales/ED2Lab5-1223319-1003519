using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Encryptors;

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
            try
            {
                string path = env.ContentRootPath + "\\" + file.FileName;
                using var saver = new FileStream(path, FileMode.Create);
                file.CopyTo(saver);
                saver.Close();
                using var fileWritten = new FileStream(path, FileMode.OpenOrCreate);
                using var reader = new BinaryReader(fileWritten);
                byte[] buffer = new byte[0];
                while (fileWritten.Position < fileWritten.Length)
                {
                    int index = buffer.Length;
                    Array.Resize<byte>(ref buffer, index + 100000);
                    byte[] aux = reader.ReadBytes(100000);
                    aux.CopyTo(buffer, index);
                }
                reader.Close();
                fileWritten.Close();
                for (int i = 0; i < buffer.Length; i++)
                {
                    if (buffer[i] == 0)
                    {
                        Array.Resize<byte>(ref buffer, i);
                        break;
                    }
                }
                if (buffer.Length > 0)
                {
                    IEncryptor encryptor;
                    FileStream fileStream;
                    switch (method)
                    {
                        case "cesar":
                            encryptor = new CesarEncryptor(env.ContentRootPath);
                            path = encryptor.Cipher(buffer, key, file.FileName);
                            fileStream = new FileStream(path, FileMode.OpenOrCreate);
                            return File(fileStream, "text/plain");
                        case "zigzag":
                            encryptor = new ZigZagEncryptor(env.ContentRootPath);
                            path = encryptor.Cipher(buffer, key, file.FileName);
                            fileStream = new FileStream(path, FileMode.OpenOrCreate);
                            return File(fileStream, "text/plain");
                        case "ruta":
                            encryptor = new RouteEncryptor(env.ContentRootPath);
                            path = encryptor.Cipher(buffer, key, file.FileName);
                            fileStream = new FileStream(path, FileMode.OpenOrCreate);
                            return File(fileStream, "text/plain");
                        default:
                            return StatusCode(500, "El método no es válido");
                    }
                }
                else
                    return StatusCode(500, "El archivo está vacío");
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("/api/decipher")]
        public IActionResult Decipher([FromForm] IFormFile file, [FromForm] string key)
        {
            try
            {
                string path = env.ContentRootPath + "\\" + file.FileName;
                using var saver = new FileStream(path, FileMode.Create);
                file.CopyTo(saver);
                saver.Close();
                using var fileWritten = new FileStream(path, FileMode.OpenOrCreate);
                using var reader = new BinaryReader(fileWritten);
                byte[] buffer = new byte[0];
                while (fileWritten.Position < fileWritten.Length)
                {
                    int index = buffer.Length;
                    Array.Resize<byte>(ref buffer, index + 100000);
                    byte[] aux = reader.ReadBytes(100000);
                    aux.CopyTo(buffer, index);
                }
                reader.Close();
                fileWritten.Close();
                for (int i = 0; i < buffer.Length; i++)
                {
                    if (buffer[i] == 0)
                    {
                        Array.Resize<byte>(ref buffer, i);
                        break;
                    }
                }
                if (buffer.Length > 0)
                {
                    string type = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                    IEncryptor encryptor;
                    FileStream fileStream;
                    switch (type)
                    {
                        case ".csr":
                            encryptor = new CesarEncryptor(env.ContentRootPath);
                            path = encryptor.Decipher(buffer, key, file.FileName);
                            fileStream = new FileStream(path, FileMode.OpenOrCreate);
                            return File(fileStream, "text/plain");
                        case ".zz":
                            encryptor = new ZigZagEncryptor(env.ContentRootPath);
                            path = encryptor.Decipher(buffer, key, file.FileName);
                            fileStream = new FileStream(path, FileMode.OpenOrCreate);
                            return File(fileStream, "text/plain");
                        case ".rt":
                            encryptor = new RouteEncryptor(env.ContentRootPath);
                            path = encryptor.Decipher(buffer, key, file.FileName);
                            fileStream = new FileStream(path, FileMode.OpenOrCreate);
                            return File(fileStream, "text/plain");
                        default:
                            return StatusCode(500, "El archivo no es de un tipo válido");
                    }
                }
                else
                    return StatusCode(500, "El archivo está vacío");
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
