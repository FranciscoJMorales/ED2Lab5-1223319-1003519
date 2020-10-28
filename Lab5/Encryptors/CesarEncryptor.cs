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

        public string ShowCipher(string text, string key)
        {
            return text;
        }

        public string Cipher(byte[] content, string key, string name)
        {
            if (KeyIsValid(key))
            {
                string text = ConvertToString(content);
                string final = ShowCipher(text, key);
                string path = Path + "\\" + name.Remove(name.LastIndexOf('.')) + ".csr";
                using var file = new FileStream(path, FileMode.Create);
                file.Write(ConvertToByteArray(final), 0, final.Length);
                return path;
            }
            else
                return "";
        }

        public string ShowDecipher(string text, string key)
        {
            return text;
        }

        public string Decipher(byte[] content, string key, string name)
        {
            if (KeyIsValid(key))
            {
                string text = ConvertToString(content);
                string final = ShowDecipher(text, key);
                string path = Path + "\\" + name.Remove(name.LastIndexOf('.')) + ".txt";
                using var file = new FileStream(path, FileMode.Create);
                file.Write(ConvertToByteArray(final), 0, final.Length);
                return path;
            }
            else
                return "";
        }

        private string ConvertToString(byte[] array)
        {
            string text = "";
            foreach (var item in array)
                text += Convert.ToString(Convert.ToChar(item));
            return text;
        }

        private bool KeyIsValid(string key)
        {
            List<char> alphabet = new List<char>();
            for (int i = 97; i < 26; i++)
                alphabet.Add(Convert.ToChar(i));
            foreach (var item in key)
            {
                if (!alphabet.Contains(item))
                    return false;
            }
            return true;
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
