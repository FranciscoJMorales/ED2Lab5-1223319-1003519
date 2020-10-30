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
            return NoContent();
        }

        [HttpPost]
        [Route("/api/cipher/{method}")]
        public IActionResult Cipher([FromForm] IFormFile file, [FromForm] string key, string method)
        {
            try
            {
                string type = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                if (type == ".txt")
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
                        path = "";
                        switch (method)
                        {
                            case "cesar":
                                encryptor = new CesarEncryptor(env.ContentRootPath);
                                path = encryptor.Cipher(buffer, key, file.FileName);
                                break;
                            case "zigzag":
                                encryptor = new ZigZagEncryptor(env.ContentRootPath);
                                path = encryptor.Cipher(buffer, key, file.FileName);
                                break;
                            case "ruta":
                                encryptor = new RouteEncryptor(env.ContentRootPath);
                                path = encryptor.Cipher(buffer, key, file.FileName);
                                break;
                            default:
                                return StatusCode(500, "El método no es válido");
                        }
                        if (path != "")
                        {
                            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
                            return File(fileStream, "text/plain");
                        }
                        else
                            return StatusCode(500, "La llave no es valida\r\nLlaves validas:\r\nCifrado Cesar: Solo puede tener letras del abecedario\r\nCifrado ZigZag: n (n = Número mayor a 0)\r\nCifrado de Ruta: tipo:nXm (tipo = vertical, espiral; n = filas mayor a 0; m = columnas mayor a 0)");
                    }
                    else
                        return StatusCode(500, "El archivo está vacío");
                }
                else
                    return StatusCode(500, "El archivo debe ser .txt");
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
                string type = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                if (type == ".csr" || type == ".zz" || type == ".rt")
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
                        path = "";
                        switch (type)
                        {
                            case ".csr":
                                encryptor = new CesarEncryptor(env.ContentRootPath);
                                path = encryptor.Decipher(buffer, key, file.FileName);
                                break;
                            case ".zz":
                                encryptor = new ZigZagEncryptor(env.ContentRootPath);
                                path = encryptor.Decipher(buffer, key, file.FileName);
                                break;
                            case ".rt":
                                encryptor = new RouteEncryptor(env.ContentRootPath);
                                path = encryptor.Decipher(buffer, key, file.FileName);
                                break;
                            default:
                                return StatusCode(500, "El archivo no es de un tipo válido");
                        }
                        if (path != "")
                        {
                            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
                            return File(fileStream, "text/plain");
                        }
                        else
                            return StatusCode(500, "La llave no es valida\r\nLlaves validas:\r\nCifrado Cesar: Solo puede tener letras del abecedario\r\nCifrado ZigZag: n (n = Número mayor a 0)\r\nCifrado de Ruta: tipo:nXm (tipo = vertical, espiral; n = filas mayor a 0; m = columnas mayor a 0)");
                    }
                    else
                        return StatusCode(500, "El archivo está vacío");
                }
                else
                    return StatusCode(500, "El archivo no es de un tipo válido");
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
