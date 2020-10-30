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
            if (vertical)
                return VerticalCipher(text, rows, columns);
            else
                return EspiralCipher(text, rows, columns);
        }

        private string VerticalCipher(string text, int rows, int columns)
        {
            char lastChar = '$';
            if (text.EndsWith('$'))
                lastChar = '|';
            while (text.Length % (rows * columns) > 0)
            {
                text += lastChar;
            }
            List<char[,]> list = new List<char[,]>();
            int pos = 0;
            while (text.Length > 0)
            {
                list.Add(new char[rows, columns]);
                for (int i = 0; i < columns; i++)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        list[pos][j, i] = text[0];
                        text = text.Remove(0, 1);
                    }
                }
                pos++;
            }
            string final = "";
            foreach(var item in list)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                        final += item[i, j];
                }
            }
            return final;
        }

        private string EspiralCipher(string text, int rows, int columns)
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
            if (vertical)
                return VerticalDecipher(text, rows, columns);
            else
                return EspiralDecipher(text, rows, columns);
        }

        private string VerticalDecipher(string text, int rows, int columns)
        {
            char lastChar = '$';
            if (text.EndsWith('|'))
                lastChar = '|';
            while (text.Length % (rows * columns) > 0)
            {
                text += lastChar;
            }
            List<char[,]> list = new List<char[,]>();
            int pos = 0;
            while (text.Length > 0)
            {
                list.Add(new char[rows, columns]);
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        list[pos][i, j] = text[0];
                        text = text.Remove(0, 1);
                    }
                }
                pos++;
            }
            string final = "";
            foreach (var item in list)
            {
                for (int i = 0; i < columns; i++)
                {
                    for (int j = 0; j < rows; j++)
                        final += item[j, i];
                }
            }
            while (final.EndsWith(lastChar))
                final = final.Remove(final.Length - 1);
            return final;
        }

        private string EspiralDecipher(string text, int rows, int columns)
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
