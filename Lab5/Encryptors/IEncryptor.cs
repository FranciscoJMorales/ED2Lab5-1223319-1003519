using System;
using System.Collections.Generic;
using System.Text;

namespace Encryptors
{
    public interface IEncryptor
    {
        public abstract string Cipher(byte[] content, string key, string name);
        public abstract string Decipher(byte[] content, string key, string name);

        private string ConvertToString(byte[] array)
        {
            string text = "";
            foreach (var item in array)
                text += Convert.ToString(Convert.ToChar(item));
            return text;
        }

        private byte[] ConvertToByteArray(string text)
        {
            byte[] array = new byte[text.Length];
            for (int i = 0; i < text.Length; i++)
                array[i] = Convert.ToByte(text[i]);
            return array;
        }
    }
}
