using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StaticComponentGenerator
{
    public class HTMLModel : PageModel
    {
        /// <summary>
        /// Where the returned content is passed to the view.
        /// </summary>
        public string RequestedContent { get; set; }

        public async Task OnGet()
        {
            PuppeteerSharpController.PuppeteerController puppeteerController = new PuppeteerSharpController.PuppeteerController();
            RequestedContent = await puppeteerController.GenerateContent(
                "ComponentOne", 
                "Test", 
                new PuppeteerSharpController.Strategies.Download.StrategyHTML()
            );
        }
    }
}