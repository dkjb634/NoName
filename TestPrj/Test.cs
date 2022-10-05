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
            if (Prac.qtdcore.MainClass.Foo()) Assert.Pass(); //no problems here
            Assert.Fail();
        }

		[Test()]
        public void Read_FileNoLinkTxtGz()
        {
				// The following part fails and throws an exception
				// The problem is that the FileNoLink.txt.gz was embedded as a link and has no <Link> defined (as list.txt.gz has)
				// <ItemGroup><EmbeddedResource Include="..\Prac.qtdcore\data\lists\FileNoLink.txt.gz"></EmbeddedResource></ItemGroup>
				var assembly = Assembly.GetExecutingAssembly();

				var resName = "";
				foreach ( var el in assembly.GetManifestResourceNames() )
					if ( el.Contains("FileNoLink.txt.gz") ) resName = el;

				if (resName.Equals("")) { Console.WriteLine("Failed to find resource"); Assert.Fail(); }

				bool Success = (assembly.GetManifestResourceStream("TestPrj.data.lists.FileNoLink.txt.gz") != null)
					&& resName == "TestPrj.data.lists.FileNoLink.txt.gz";

				if ( Success ) Assert.Pass();
				Assert.Fail();
			}

		[Test()]
		public void Read_listTxtGz()
		{
			// The following part works fine
			// list.txt.gz is embedded outside of project folder and has <Link> set, so directories.path is presented in resource name.
			var assembly = Assembly.GetExecutingAssembly();

			var resName = "";
			foreach ( var el in assembly.GetManifestResourceNames() )
				if ( el.Contains("list.txt.gz") ) resName = el;

			if (resName.Equals("")) { Console.WriteLine("Failed to find resource"); Assert.Fail(); }

			bool Success = (assembly.GetManifestResourceStream("TestPrj.data.lists.list.txt.gz") != null)
				&& resName == "TestPrj.data.lists.list.txt.gz";

			if ( Success ) Assert.Pass();
			Assert.Fail();

		}

		[Test()]
		public void AccessAnotherAssembly() {
			var assembly = Assembly.GetAssembly(typeof(Prac.qtdcore.MainClass));
			var resNames = assembly.GetManifestResourceNames();

			int SuccessCounter = 0;
			foreach ( var el in resNames ) {
				if ( el.Equals("Prac.qtdcore.data.lists.FileNoLink.txt.gz") ) SuccessCounter++;
				if ( el.Equals("Prac.qtdcore.data.lists.list.txt.gz") ) SuccessCounter++;
				if ( el.Equals("Prac.qtdcore.FolderWithFile.File.txt.gz") ) SuccessCounter++;
			}

			if ( SuccessCounter == 3 ) Assert.Pass();
			Assert.Fail();
		}
    }
}
