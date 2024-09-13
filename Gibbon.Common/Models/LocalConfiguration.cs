using System;
using System.IO;

namespace Gibbon.Common.Models
{
    public class LocalConfiguration
    {
        private string _configFile => Path.Combine(Environment.CurrentDirectory, @"AppSettings.json");

        public LocalConfiguration()
        {
            if (File.Exists(_configFile) == false)
            {
                return;
            }
            
            
        }
    }
}