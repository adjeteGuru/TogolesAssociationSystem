using NUnit.Framework;
using OpenQA.Selenium;
using System.Text;
using TechTalk.SpecFlow.Tracing;

namespace TogoleseSolidarity.IntegrationTests.Helpers
{
    public static class ScreenshotHelper
    {
        public static void TakeScreenshot(ScenarioContext scenarioContext)
        {

            try
            {
                var fileNameBase =
                    $"error_{scenarioContext.ScenarioInfo.Title.ToIdentifier().Substring(0, 10)}_{DateTime.UtcNow:yyyyMMdd_HHmmss}";
                var artifactDirectory = Path.Combine(TestContext.CurrentContext.TestDirectory, "testresults");

                Directory.CreateDirectory(artifactDirectory);

                var pageSource = BrowserHelper.WebDriver.PageSource;
                var sourceFilePath = Path.Combine(artifactDirectory, fileNameBase + "_source.html");

                File.WriteAllText(sourceFilePath, pageSource, Encoding.UTF8);
                Console.WriteLine(@"Page source: {0}", new Uri(sourceFilePath));

                if (!(BrowserHelper.WebDriver is ITakesScreenshot takesScreenshot))
                {
                    return;
                }

                var screenshot = takesScreenshot.GetScreenshot();
                var screenshotFilePath = Path.Combine(artifactDirectory, fileNameBase + "_screenshot.png");
                screenshot.SaveAsFile(screenshotFilePath, ScreenshotImageFormat.Png);
                Console.WriteLine(@"Screen shot: {0}", new Uri(screenshotFilePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Error while taking screen shot: {0}", ex);
            }
        }

    }
}
