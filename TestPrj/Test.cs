using NUnit.Framework;
using System.Reflection;
using System;
using System.IO;

namespace TestPrj
{
    [TestFixture()]
    public class Test
    {
        [Test()]
        public void TestCallFooFromProject()
        {
            Console.WriteLine("Read resource through the project reference via foo");
            if (Prac.qtdcore.MainClass.Foo()) Assert.Pass();
            Assert.Fail();
        }

        [Test()]
        public void TestReadResInUTProject()
        {
            Console.WriteLine("Read resource through via linked embedded resource");
            if (CopiedFoo()) Assert.Pass();
            Assert.Fail();

        }

        public bool CopiedFoo()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                string listtxtgz = assembly.GetManifestResourceNames()[0];
                Console.WriteLine("Read resource: " + listtxtgz);
                using (Stream stream = assembly.GetManifestResourceStream(
                    "TestPrj.data.lists.list.txt.gz"))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string data = reader.ReadToEnd();
                    Console.WriteLine("data: " + data);
                }

                string filenolinktxtgz = assembly.GetManifestResourceNames()[1];
                Console.WriteLine("Read resource: " + filenolinktxtgz);
                using (Stream stream = assembly.GetManifestResourceStream(
                           "TestPrj.data.lists.FileNoLink.txt.gz"))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string data = reader.ReadToEnd();
                    Console.WriteLine("data: " + data);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
            return true;

        }
    }
}
