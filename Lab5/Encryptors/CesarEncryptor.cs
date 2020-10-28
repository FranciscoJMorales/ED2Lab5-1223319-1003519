using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Encryptors
{
    public class CesarEncryptor : IEncryptor
    {
        private readonly string Path;

        public CesarEncryptor(string path)
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
