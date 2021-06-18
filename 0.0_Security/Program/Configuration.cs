using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Security
{
    class Configuration
    {
        private static Regex dataRegex = new Regex(
            "[a-zA-Z0-9_]=\"([a-zA-Z0-9:]*)\""
        );

        public static Dictionary<string, string> Variables { get;set; }
        public static async Task InitializeFileVariables(string envFilePath)
        {
            foreach(var line in await File.ReadAllLinesAsync(envFilePath))
            {
                var split = line.Split('='); // EnvKey:EnvData
                Variables.Add(
                    split[0],
                    dataRegex
                        .Match(split[1])
                        .Groups[0]
                        .Value
                );
            }
        }
    }
}