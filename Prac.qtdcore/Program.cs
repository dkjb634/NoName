using System;
using System.IO;
using System.Reflection;

namespace Prac.qtdcore
{
    public class MainClass
    {
        public static bool Foo()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                Console.WriteLine("Read resource: " + assembly.GetManifestResourceNames()[0]);
                using (Stream stream = assembly.GetManifestResourceStream(
                    "Prac.qtdcore.data.lists.list.txt.gz"))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string data = reader.ReadToEnd();
                    Console.WriteLine("data: " + data);
                }
            } catch(Exception ex) { Console.WriteLine(ex.Message); return false; }
            return true;

        }
        public static void Main(string[] args)
        {

			Console.WriteLine((Foo())?"success":"fail");
        }
    }
}
