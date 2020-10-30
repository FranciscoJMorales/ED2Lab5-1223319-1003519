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
            if (rows == 1)
                return text;
            else
            {
                char lastChar = '$';
                if (text.EndsWith('$'))
                    lastChar = '|';
                while (text.Length % (2 * (rows - 1)) > 0)
                {
                    text += lastChar;
                }
                List<char>[] list = new List<char>[rows];
                for (int j = 0; j < list.Length; j++)
                    list[j] = new List<char>();
                int i = 0;
                bool direction = true;
                while (text.Length > 0)
                {
                    list[i].Add(text[0]);
                    text = text.Remove(0, 1);
                    if (direction)
                    {
                        if (i == rows - 1)
                        {
                            i--;
                            direction = false;
                        }
                        else
                            i++;
                    }
                    else
                    {
                        if (i == 0)
                        {
                            i++;
                            direction = true;
                        }
                        else
                            i--;
                    }
                }
                string final = "";
                foreach (var item2 in list)
                {
                    foreach (var item in item2)
                        final += item;
                }
                return final;
            }
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
