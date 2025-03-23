using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace TogoleseSolidarity.IntegrationTests.Helpers
{
    public static class BrowserHelper
    {
        public static IWebDriver WebDriver { get; private set; }
        private static readonly bool RunningOnBuildAgent =
            Environment.GetEnvironmentVariable("AGENT_BUILDDIRECTORY") != null;

        public static void StartBrowser()
        {
            WebDriver = StartChrome();
        }

        public static void StopBrowser()
        {
            WebDriver.Close();
            WebDriver.Dispose();
            WebDriver = null;
        }

        public static void ClearCookies()
        {
            WebDriver?.Manage().Cookies.DeleteAllCookies();
        }

        public static IWebElement FindElementWithWaits(By selector, int waitTime)
        {
            try
            {
                var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(waitTime));
                wait.Until(x => x.FindElement(selector).Displayed);
                return WebDriver.FindElement(selector);
            }
            catch (WebDriverTimeoutException ex)
            {

                throw new WebDriverTimeoutException($"Element not found before timeout: {ex}");
            }
        }

        public static IWebElement FindElementWithDesiredText(By selector, int waitTime, string desiredText)
        {
            try
            {
                var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(waitTime));
                wait.Until(x => x.FindElement(selector).Text.Contains(desiredText));
                return WebDriver.FindElement(selector);
            }
            catch (WebDriverTimeoutException ex)
            {

                throw new WebDriverTimeoutException($"Element not found before timeout: {ex}");
            }
        }

        public static IReadOnlyCollection<IWebElement> FindElementsWithWaits(By locator, int waitTime)
        {
            try
            {
                var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(waitTime));
                wait.Until(x => x.FindElement(locator).Displayed);
                
                return WebDriver.FindElements(locator);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Element not found before timeout: {ex}");
                throw;
            }
        }

        public static ReadOnlyCollection<IWebElement> VisibilityOfAllElementsLocatedBy(By locator)
        {
            try
            {
                var elements = WebDriver.FindElements(locator);
               if (elements.Any(element => !element.Displayed))
                {
                    return null;
                }

                return elements.Any() ? elements : null;
            }
            catch (StaleElementReferenceException)
            {
                return null;
            }
        }

        private static IWebDriver StartChrome()
        {
            IWebDriver chromeDriver;

            if (RunningOnBuildAgent)
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("disable-gpu");
                chromeOptions.AddArgument("--allow-insecure-localhost");
                chromeOptions.AddArgument("--lang=en-GB");

                chromeDriver = new ChromeDriver(chromeOptions);
                chromeDriver.Manage().Window.Size = new System.Drawing.Size(1600, 1200);
            }
            else
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--allow-insecure-localhost");
                chromeOptions.AddArgument("--lang=en-GB");

                chromeDriver = new ChromeDriver(chromeOptions);
                chromeDriver.Manage().Window.Maximize();
            }

            return chromeDriver;
        }
    }
}
