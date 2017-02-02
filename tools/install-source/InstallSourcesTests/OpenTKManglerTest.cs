﻿using System;
using NUnit.Framework;

using InstallSources;

namespace InstallSourcesTests
{
	[TestFixture]
	public class OpenTKManglerTest
	{
		OpenTKSourceMangler mangler;
		string openTKPath;
		string installDir;

		[SetUp]
		public void SetUp ()
		{
			openTKPath = "/Users/test/xamarin-macios/external/opentk/Source/";
			installDir = "/Users/test/xamarin-macios/_ios-build//Library/Frameworks/Xamarin.iOS.framework/Versions/git";
			mangler = new OpenTKSourceMangler {
				InstallDir = installDir,
				OpenTKSourcePath = openTKPath
			};
		}

		[TestCase ("/Users/test/xamarin-macios/external/opentk/Source/OpenTK/Math/BezierCurve.cs")]
		[TestCase ("/Users/test/xamarin-macios/external/opentk/Source/OpenTK/Math/BezierCurveCubic.cs")]
		public void TestGetSourcePath (string path)
		{
			Assert.AreEqual (path, mangler.GetSourcePath (path), "Failed getting path for '{0}'", path);
		}

		[TestCase ("/Users/test/xamarin-macios/external/opentk/Source/OpenTK/Math/BezierCurve.cs")]
		[TestCase ("/Users/test/xamarin-macios/external/opentk/Source/OpenTK/Math/BezierCurveCubic.cs")]
		public void TestGetTargetPath (string path)
		{
			var targetPath = mangler.GetTargetPath (path);
			Assert.IsFalse (targetPath.StartsWith (openTKPath, StringComparison.InvariantCulture), "Path starts with the opentk path.");
			Assert.IsTrue (targetPath.StartsWith (installDir, StringComparison.InvariantCulture), "Path does not start with the install dir");
			Assert.IsTrue (targetPath.Contains ("/src/OpenTK/"), "Path does not contain 'src'");
		}
	}
}
