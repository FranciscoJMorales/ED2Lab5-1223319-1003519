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
                Console.WriteLine("Ingrese el método para el cifrado de Ruta: 1 -> Vertical, 2 -> Espiral");
                int method = int.Parse(Console.ReadLine());
                bool vertical = true;
                if (method == 2)
                    vertical = false;
                else if (method != 1)
                {
                    Console.WriteLine("Ha ocurrido un error.");
                    Main(args);
                }
                var cesar = new CesarEncryptor("..//..//..");
                Console.WriteLine("Cifrado César:");
                Console.WriteLine(cesar.ShowCipher(text, key));
                var zigzag = new ZigZagEncryptor("..//..//..");
                Console.WriteLine("Cifrado ZigZag:");
                Console.WriteLine(zigzag.ShowCipher(text, rows));
                var route = new RouteEncryptor("..//..//..");
                Console.WriteLine("Cifrado de Ruta:");
                Console.WriteLine(route.ShowCipher(text, x, y, vertical));
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
