using System;
using System.Collections.Generic;
using System.Text;

namespace Encryptors
{
    public class RouteEncryptor : IEncryptor
    {
        private readonly string Path;

        public RouteEncryptor(string path)
        {
            Path = path;
        }

        public string Cipher(byte[] content, string key, string name)
        {
            throw new NotImplementedException();
        }

        public string Decipher(byte[] content, string key, string name)
        {
            throw new NotImplementedException();
        }
    }
}
