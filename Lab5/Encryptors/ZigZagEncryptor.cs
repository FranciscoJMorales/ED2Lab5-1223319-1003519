using System;
using System.Collections.Generic;
using System.IO;
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

        public string ShowCipher(string text, int rows)
        {
            return text;
        }

        public string Cipher(byte[] content, string key, string name)
        {
            try
            {
                int rows = int.Parse(key);
                string text = ConvertToString(content);
                if (rows > 0)
                {
                    string final = ShowCipher(text, int.Parse(key));
                    string path = Path + "\\" + name.Remove(name.LastIndexOf('.')) + ".zz";
                    using var file = new FileStream(path, FileMode.Create);
                    file.Write(ConvertToByteArray(final), 0, final.Length);
                    return path;
                }
                else
                    return "";
            }
            catch
            {
                return "";
            }
        }

        public string ShowDecipher(string text, int rows)
        {
            return text;
        }

        public string Decipher(byte[] content, string key, string name)
        {
            try
            {
                int rows = int.Parse(key);
                if (rows > 0)
                {
                    string text = ConvertToString(content);
                    string final = ShowDecipher(text, rows);
                    string path = Path + "\\" + name.Remove(name.LastIndexOf('.')) + ".txt";
                    using var file = new FileStream(path, FileMode.Create);
                    file.Write(ConvertToByteArray(final), 0, final.Length);
                    return path;
                }
                else
                    return "";
            }
            catch
            {
                return "";
            }
        }

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
