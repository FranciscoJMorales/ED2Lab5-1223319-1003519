using System;
using Encryptors;

namespace TestEncryptors
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Ingrese el texto por cifrar:");
                string text = Console.ReadLine();
                Console.WriteLine("Ingrese la palabra clave para el cifrado César:");
                string key = Console.ReadLine();
                Console.WriteLine("Ingrese el número de filas para el cifrado ZigZag:");
                int rows = int.Parse(Console.ReadLine());
                Console.WriteLine("Ingrese el número de filas para el cifrado de Ruta:");
                int x = int.Parse(Console.ReadLine());
                Console.WriteLine("Ingrese el número de columnas para el cifrado de Ruta:");
                int y = int.Parse(Console.ReadLine());
                var cesar = new CesarEncryptor("..//..//..");
                Console.WriteLine("Cifrado César:");
                Console.WriteLine(cesar.ShowCipher(text, key));
                Console.WriteLine(cesar.ShowDecipher(cesar.ShowCipher(text, key), key));
                Console.WriteLine();
                var zigzag = new ZigZagEncryptor("..//..//..");
                Console.WriteLine("Cifrado ZigZag:");
                Console.WriteLine(zigzag.ShowCipher(text, rows));
                Console.WriteLine(zigzag.ShowDecipher(zigzag.ShowCipher(text, rows), rows));
                Console.WriteLine();
                var route = new RouteEncryptor("..//..//..");
                Console.WriteLine("Cifrado de Ruta vertical:");
                Console.WriteLine(route.ShowCipher(text, x, y, true));
                Console.WriteLine(route.ShowDecipher(route.ShowCipher(text, x, y, true), x, y, true));
                Console.WriteLine();
                Console.WriteLine("Cifrado de Ruta espiral:");
                Console.WriteLine(route.ShowCipher(text, x, y, false));
                Console.WriteLine(route.ShowDecipher(route.ShowCipher(text, x, y, false), x, y, false));
                Console.ReadKey();
            }
            catch
            {
                Console.WriteLine("Ha ocurrido un error.");
                Main(args);
            }
        }
    }
}
