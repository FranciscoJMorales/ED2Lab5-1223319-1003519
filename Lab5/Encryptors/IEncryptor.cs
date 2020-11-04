using System;
using System.Collections.Generic;
using System.Text;

namespace Encryptors
{
    public interface IEncryptor
    {
        public abstract string Cipher(byte[] content, Key key, string name);
        public abstract string Decipher(byte[] content, Key key, string name);
    }
}
