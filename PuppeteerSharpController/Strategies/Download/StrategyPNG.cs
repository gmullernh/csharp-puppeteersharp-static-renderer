using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PuppeteerSharpController.Strategies.Download
{
    public class StrategyPNG : IStrategy
    {
        public Page PuppeteerPage { get; set; }

        public async Task<string> Execute()
        {
            try
            {            
                // Configures the screenshot
                ScreenshotOptions screenshotOptions = new ScreenshotOptions
                {
                    Type = ScreenshotType.Png,
                    OmitBackground = true
                };

                // The file path to be saved
                string filename = $"{Environment.CurrentDirectory}/wwwroot/{DateTime.Now.ToString("yyyyMMdd_hhmmss")}.png";
                await PuppeteerPage.ScreenshotAsync(filename, screenshotOptions);
                
                // returns only the last element (filename + extension).
                return filename.Split('/')[^1];
            }
            catch (AggregateException ex)
            {
                return $"Couldn't generate the content due to error {ex.Message}";
            }
        }
    }
}
