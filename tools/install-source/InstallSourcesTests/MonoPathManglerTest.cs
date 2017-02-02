﻿using System;
using NUnit.Framework;

using InstallSources;

namespace InstallSourcesTests
{
	[TestFixture]
	public class MonoPathManglerTest
	{
		MonoPathMangler mangler;
		string monoPath;
		string installDir;

		[SetUp]
		public void SetUp()
		{
			monoPath = "/Users/test/xamarin-macios/external/mono/";
			installDir = "/Users/test/xamarin-macios/_ios-build//Library/Frameworks/Xamarin.iOS.framework/Versions/git";
			mangler = new MonoPathMangler { 
				InstallDir = installDir,
				MonoSourcePath = monoPath 
			};
		}

		[Test]
		public void TestGetSourcePath (
			[Values ("/Users/test/xamarin-macios/external/mono/mcs/class/Mono.Data.Tds/Mono.Data.Tds.Protocol/TdsAsyncState.cs",
			         "/Users/test/xamarin-macios/external/mono/mcs/class/Mono.Security/Mono.Security.X509/X509StoreManager.cs",
			         "/Users/test/xamarin-macios/external/mono/mcs/class/dlr/Runtime/Microsoft.Scripting.Core/Actions/UpdateDelegates.Generated.cs")]
			string path)
		{
			Assert.AreEqual (path, mangler.GetSourcePath (path), "Failed getting path for '{0}'", path);
		}

		[Test]
		public void TestGetTargetPath(
			[Values ("/Users/test/xamarin-macios/external/mono/mcs/class/Mono.Data.Tds/Mono.Data.Tds.Protocol/TdsAsyncState.cs",
					 "/Users/test/xamarin-macios/external/mono/mcs/class/Mono.Security/Mono.Security.X509/X509StoreManager.cs",
					 "/Users/test/xamarin-macios/external/mono/mcs/class/dlr/Runtime/Microsoft.Scripting.Core/Actions/UpdateDelegates.Generated.cs")]
			string path)
		{
			var targetPath = mangler.GetTargetPath (path);
			Assert.IsFalse (targetPath.StartsWith (monoPath, StringComparison.InvariantCulture), "Path starts with the mono path.");
			Assert.IsTrue (targetPath.StartsWith (installDir, StringComparison.InvariantCulture), "Path does not start with the install dir");
			Assert.IsTrue (targetPath.Contains ("/src/mono/"), "Path does not contain 'src'");
		}
	}
}
