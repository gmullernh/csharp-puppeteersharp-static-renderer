using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PuppeteerSharpController.Strategies.Download
{
    public class StrategyHTML : IStrategy
    {
        public Page PuppeteerPage { get; set; }

        public async Task<string> Execute()
        {
            try
            {
                Console.WriteLine("Returning HTML");
                return await PuppeteerPage.GetContentAsync();
            }
            catch (AggregateException ex)
            {
                PuppeteerPage.Dispose();
                Console.WriteLine($"Returning HTML - An error ocurred {ex.Message}.");
                return $"Couldn't load content {ex.Message}";
            }
        }
    }
}
