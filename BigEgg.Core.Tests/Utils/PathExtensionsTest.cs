namespace BigEgg.Tests.Utils
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class PathExtensionsTest
    {
        [TestClass]
        public class GetRelativePathTest
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void FromPathNull()
            {
                PathExtensions.GetRelativePath(null, @"D:\Windows\regedit.exe");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void FromPathNotValid()
            {
                @"dows\Web\Wallpaper\".GetRelativePath(@"D:\Windows\regedit.exe");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void ToPathNull()
            {
                PathExtensions.GetRelativePath(@"D:\Windows\Web\Wallpaper\", null);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void ToPathNotValid()
            {
                @"D:\Windows\Web\Wallpaper\".GetRelativePath(@"ndows\regedit.exe");
            }

            [TestMethod]
            public void GetRelativePath()
            {
                string relativePath = @"D:\Windows\Web\Wallpaper\".GetRelativePath(@"D:\Windows\regedit.exe");
                Assert.AreEqual(@"..\..\regedit.exe", relativePath);
            }
        }
    }
}
