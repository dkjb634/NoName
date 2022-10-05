using NUnit.Framework;
using System.Reflection;
using System;
using System.IO;
using System.Linq;

namespace Prac.qtdcore.TestInSameProject {

	[TestFixture()]
	public class Test {
		[Test()]
		public void TestCallResourcesFromSameProject() {
			var assembly = Assembly.GetExecutingAssembly();

			var resName = "";
			foreach ( var el in assembly.GetManifestResourceNames() )
				if ( el.Contains("File.txt.gz") ) resName = el;
			if (resName.Equals("")) { Console.WriteLine("Failed to find resource"); Assert.Fail(); }

			bool Success = (Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Prac.qtdcore.FolderWithFile.File.txt.gz") != null)
				&& resName == "Prac.qtdcore.FolderWithFile.File.txt.gz";

			if ( Success ) Assert.Pass();
			Assert.Fail();
		}
	}
}