using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PuppeteerSharpController
{
    internal class PuppeteerInstaller
    {
        private static readonly string _CHROMIUM_INSTALL_PATH = $"{Environment.CurrentDirectory}/App_Data";
        private const int _CHROMIUM_VERSION = BrowserFetcher.DefaultRevision;

        internal static readonly string CHROMIUM_EXECUTION_PATH = _CHROMIUM_INSTALL_PATH + $"/Win64-{_CHROMIUM_VERSION}/chrome-win/chrome.exe";
        internal static readonly string CHROMIUM_TEMP_DATA_DIR = $"{Environment.CurrentDirectory}/App_Data/Temp";

        internal async Task DownloadAndInstall()
        {
            // Creates the installation folder
            CreateInstallationFolderIfNotExists(_CHROMIUM_INSTALL_PATH);
            // Creates the Temp Folder
            CreateInstallationFolderIfNotExists(CHROMIUM_TEMP_DATA_DIR);

            // If Chromium isn't installed
            if(!File.Exists(CHROMIUM_EXECUTION_PATH))
            {
                // Defines the path for installation within the application
                BrowserFetcherOptions bf = new BrowserFetcherOptions
                {
                    Path = _CHROMIUM_INSTALL_PATH,
                    Platform = Platform.Win64
                };

                Console.WriteLine($"Installing Chromium - version {_CHROMIUM_VERSION} at {_CHROMIUM_INSTALL_PATH}.");
                // Download and install
                await new BrowserFetcher(bf).DownloadAsync(_CHROMIUM_VERSION);
            }
        }

        /// <summary>
        /// Creates the installation folder
        /// </summary>
        private void CreateInstallationFolderIfNotExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

    }
}
