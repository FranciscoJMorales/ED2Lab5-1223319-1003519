using System;
using System.Collections.Generic;
using System.Text;

namespace Encryptors
{
    public interface IEncryptor
    {
        public abstract string Cipher(byte[] content, string key, string name);
        public abstract string Decipher(byte[] content, string key, string name);
    }
}
