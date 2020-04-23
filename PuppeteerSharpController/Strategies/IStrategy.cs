using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PuppeteerSharpController.Strategies
{
    public interface IStrategy
    {
        /// <summary>
        /// The Puppeteer Page in which the action will be executed
        /// </summary>
        Page PuppeteerPage { get; set; }

        /// <summary>
        /// The action that will be executed
        /// </summary>
        /// <returns>The generated content as string</returns>
        Task<string> Execute();
    }
}
