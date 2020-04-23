using System;
using System.IO;
using System.Threading.Tasks;
using PuppeteerSharp;
using PuppeteerSharpController.Strategies;

namespace PuppeteerSharpController
{
    public class PuppeteerController
    {
        /// <summary>
        /// Creates a controller/interface for controlling Puppeteer.
        /// </summary>
        public PuppeteerController() { }

        /// <summary>
        /// Generates the result based on the component name, located at \Components\, the data and the return strategy.
        /// </summary>
        /// <param name="componentName">\Components\$componentName\index.html</param>
        /// <param name="mydata">the data</param>
        /// <param name="strategy">from PuppeteerSharpController.Strategies.Download</param>
        /// <returns>returns the response as a string</returns>
        public async Task<string> GenerateContent(string componentName, string mydata, IStrategy strategy)
        {
            if (string.IsNullOrEmpty(componentName) || string.IsNullOrEmpty(mydata) || strategy == null)
                return "Invalid request";

            // Create the Browser context
            using Browser browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,    // No UI. If you want to debug, change it to false.
                ExecutablePath = PuppeteerInstaller.CHROMIUM_EXECUTION_PATH,
                IgnoreHTTPSErrors = true,   // ONLY if you're using internal resources only.
                Timeout = (int)TimeSpan.FromMinutes(3).TotalMilliseconds,
                UserDataDir = PuppeteerInstaller.CHROMIUM_TEMP_DATA_DIR 
                // The PuppeteerSharp Data Directory, 
                // if you don't specify it, PuppeteerSharp will create TEMP folders for each session 
                // at a system \Temp folder, which can exceed the system space
            });

            // Create the page context
            using Page page = await browser.NewPageAsync();
            Console.WriteLine($"Opening page: {page.Url}");

            await page.SetJavaScriptEnabledAsync(true);
            await page.SetBypassCSPAsync(true);

            // Sets the data as a DOM variable
            await page.EvaluateExpressionAsync($"var mydata = '{mydata}'");

            // Sets the path of the component's entry point
            string sourceIndexPath = $"{Environment.CurrentDirectory}/Components/{componentName}/index.html";
            await page.SetContentAsync(ReadFile(sourceIndexPath));

            // Sets the strategy page to be the current Puppeteer Page
            strategy.PuppeteerPage = page;

            // Executes the strategy
            return await strategy.Execute();
        }

        /// <summary>
        /// Installs Puppeteer
        /// </summary>
        public static async Task InstallPuppeteer()
        {
            PuppeteerInstaller puppeteerInstaller = new PuppeteerInstaller();
            await puppeteerInstaller.DownloadAndInstall();
        }

        /// <summary>
        /// Reads the content of a file
        /// </summary>
        private string ReadFile(string filepath)
        {
            try
            {
                return File.ReadAllText(filepath);
            }
            catch (AggregateException ex)
            {
                Console.WriteLine($"Error while reading the HTML file: {ex.Message}.");
                return string.Empty;
            }
        }

    }
}
