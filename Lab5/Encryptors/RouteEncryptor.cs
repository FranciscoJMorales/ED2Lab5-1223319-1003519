using System;
using System.Collections.Generic;
using System.IO;
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

        public string ShowCipher(string text, int rows, int columns, bool vertical)
        {
            return text;
        }

        public string Cipher(byte[] content, string key, string name)
        {
            try
            {
                string[] keys = key.Split(':');
                string[] size = keys[1].Split('X');
                bool vertical = true;
                if (keys[0] == "espiral")
                    vertical = false;
                else if (keys[0] != "vertical")
                    return "";
                int rows = int.Parse(size[0]);
                int columns = int.Parse(size[1]);
                if (rows > 0 && columns > 0)
                {
                    string text = ConvertToString(content);
                    string final = ShowCipher(text, rows, columns, vertical);
                    string path = Path + "\\" + name.Remove(name.LastIndexOf('.')) + ".rt";
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

        public string ShowDecipher(string text, int rows, int columns, bool vertical)
        {
            return text;
        }

        public string Decipher(byte[] content, string key, string name)
        {
            try
            {
                string[] keys = key.Split(':');
                string[] size = keys[1].Split('X');
                bool vertical = true;
                if (keys[0] == "espiral")
                    vertical = false;
                else if (keys[0] != "vertical")
                    return "";
                int rows = int.Parse(size[0]);
                int columns = int.Parse(size[1]);
                if (rows > 0 && columns > 0)
                {
                    string text = ConvertToString(content);
                    string final = ShowDecipher(text, rows, columns, vertical);
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
