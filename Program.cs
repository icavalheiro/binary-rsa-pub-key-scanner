using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("you must specify the path to the binary file");
                return;
            }

            var strings = new List<string>();
            var path = args[0];
            var validChars = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
            var currentStringBuilder = new StringBuilder();
            using (var file = File.OpenRead(path))
            {
                while (file.Position < file.Length)
                {
                    var c = (char)file.ReadByte();
                    if (validChars.IndexOf(c) >= 0)
                    {
                        currentStringBuilder.Append(c);
                    }
                    else
                    {
                        if (currentStringBuilder.Length > 0)
                        {
                            strings.Add(currentStringBuilder.ToString());
                            currentStringBuilder = new StringBuilder();
                        }
                    }
                }
            }

            if (currentStringBuilder.Length > 0)
            {
                strings.Add(currentStringBuilder.ToString());
            }

            Console.WriteLine("Possible RSA strings found:");
            var query = strings.Where(x => x.Length == 256);

            if (query.Count() == 0)
            {
                Console.WriteLine("No possible RSA strings found =S");
            }

            foreach (var s in query)
            {
                Console.WriteLine("> " + s);
            }
        }
    }
}
