namespace BigEgg.Tests.Progress
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Progress;

    [TestClass]
    public class ProgressReportTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var report = new ProgressReport(10, 25);
            Assert.AreEqual(10, report.Current);
            Assert.AreEqual(25, report.Total);
            Assert.AreEqual(40, report.Percentage);
            Assert.IsTrue(string.IsNullOrWhiteSpace(report.Message));

            report = new ProgressReport(15, 25, "Some Message");
            Assert.AreEqual(15, report.Current);
            Assert.AreEqual(25, report.Total);
            Assert.AreEqual(60, report.Percentage);
            Assert.AreEqual("Some Message", report.Message);
        }
    }
}
