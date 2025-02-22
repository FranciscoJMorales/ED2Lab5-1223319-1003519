﻿using System;
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

        public string Cipher(byte[] content, Key key, string name)
        {
            try
            {
                if (key.Levels > 0)
                {
                    string text = ConvertToString(content);
                    string final = ShowCipher(text, key.Levels);
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
            if (rows == 1)
                return text;
            else
            {
                char lastChar = '$';
                if (text.Contains('|'))
                    lastChar = '|';
                while (text.Length % (2 * (rows - 1)) > 0)
                {
                    text += lastChar;
                }
                int cycles = text.Length / (2 * (rows - 1));
                int length = text.Length;
                List<char>[] list = new List<char>[rows];
                for (int j = 0; j < list.Length; j++)
                {
                    list[j] = new List<char>();
                    int times = 1;
                    if (j > 0 && j < rows - 1)
                        times++;
                    for (int k = 0; k < (cycles * times); k++)
                    {
                        list[j].Add(text[0]);
                        text = text.Remove(0, 1);
                    }
                }
                string final = "";
                int i = 0;
                bool direction = true;
                while (final.Length < length)
                {
                    final += list[i][0];
                    list[i].RemoveAt(0);
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
                while (final.EndsWith(lastChar))
                    final = final.Remove(final.Length - 1);
                return final;
            }
        }

        public string Decipher(byte[] content, Key key, string name)
        {
            try
            {
                if (key.Levels > 0)
                {
                    string text = ConvertToString(content);
                    string final = ShowDecipher(text, key.Levels);
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
