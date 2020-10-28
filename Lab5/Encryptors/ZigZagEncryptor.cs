using System;
using System.Collections.Generic;
using System.Text;

namespace Encryptors
{
    public class ZigZagEncryptor : IEncryptor
    {
        private readonly string Path;

        public ZigZagEncryptor(string path)
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
