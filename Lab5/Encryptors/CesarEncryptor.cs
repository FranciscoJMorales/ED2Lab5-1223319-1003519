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
            List<char> alphabetMayus = Alphabet(true);
            List<char> alphabetMinus = Alphabet(false);
            List<char> kalphabetMayus = Alphabet(key.ToUpper(),true);        
            List<char> kalphabetMinus = Alphabet(key.ToLower(), false);
            string final = "";
          


            foreach (var item in text)
            {
              
                if (alphabetMayus.Contains(item))
                {
                    final += kalphabetMayus[alphabetMayus.IndexOf(item)];
                }
                else if(alphabetMinus.Contains(item))
                {
                    final += kalphabetMinus[alphabetMinus.IndexOf(item)];

                }
                else
                {
                    final += item;
                }
                                                      
            }

            alphabetMayus.Clear();
            alphabetMinus.Clear();
            kalphabetMayus.Clear();
            kalphabetMinus.Clear();

            return final;
        }

        public string Cipher(byte[] content, string key, string name)
        {
            if (KeyIsValid(key.ToUpper()))
            {
                string text = ConvertToString(content);
                string final = ShowCipher(text, key.ToUpper());
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
            List<char> alphabetMayus = Alphabet(true);
            List<char> alphabetMinus = Alphabet(false);
            List<char> kalphabetMayus = Alphabet(key.ToUpper(), true);
            List<char> kalphabetMinus = Alphabet(key.ToLower(), false);
            string final = "";



            foreach (var item in text)
            {

                if (kalphabetMayus.Contains(item))
                {
                    final += alphabetMayus[kalphabetMayus.IndexOf(item)];
                }
                else if (kalphabetMinus.Contains(item))
                {
                    final += alphabetMinus[kalphabetMinus.IndexOf(item)];

                }
                else
                {
                    final += item;
                }

            }

            alphabetMayus.Clear();
            alphabetMinus.Clear();
            kalphabetMayus.Clear();
            kalphabetMinus.Clear();

            return final;
        }

        public string Decipher(byte[] content, string key, string name)
        {
            if (KeyIsValid(key.ToUpper()))
            {
                string text = ConvertToString(content);
                string final = ShowDecipher(text, key.ToUpper());
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
            List<char> alphabet = Alphabet(true);
            foreach (var item in key)
            {
                if (!alphabet.Contains(item))
                    return false;
            }
            return true;
        }

        private List<char> Alphabet(bool mayus)
        {
            List<char> alphabet = new List<char>();
            if (mayus)
            {
                for (int i = 65; i < 91; i++)
                    alphabet.Add(Convert.ToChar(i));
            }
            else
            {
                for (int i = 97; i < 123; i++)
                    alphabet.Add(Convert.ToChar(i));

            }
            

            return alphabet;
        }

        private List<char> Alphabet(string key, bool mayus)
        {
            List<char> alphabet = new List<char>();
            if (mayus)
            {
                foreach (var item in key)
                    if (!alphabet.Contains(item))
                        alphabet.Add(item);
               
                for (int i = 65; i < 91; i++)
                    if (!alphabet.Contains(Convert.ToChar(i)))
                        alphabet.Add(Convert.ToChar(i));                
            }
            else {

                foreach (var item in key)
                    if (!alphabet.Contains(item))
                        alphabet.Add(item);

                for (int i = 97; i < 123; i++)
                    if (!alphabet.Contains(Convert.ToChar(i)))
                        alphabet.Add(Convert.ToChar(i));
            }
            

         

            return alphabet;
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
