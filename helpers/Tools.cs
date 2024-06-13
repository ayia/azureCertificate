using Azurtesting.Models;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Azurtesting.helpers
{
    public class Tools
    {
        public Scenario ParseScenario(string text, string scenarioType)
        {
            Scenario scenario = new Scenario();
            // Ajustement de l'expression régulière pour capturer correctement les scénarios
            string pattern = scenarioType + @"\s*:\s*(consider\s+(short|long)\s+positions\s+from\s+corrections\s+(below|above)\s+the\s+level\s+of\s+(\d+\.\d+|\d+)\s+with\s+a\s+target\s+of\s+(\d+\.\d+|\d+)\s*(&ndash;|–|-)\s*(\d+\.\d+|\d+)|breakout and consolidation\s+(above|below)\s+the\s+level\s+of\s+(\d+\.\d+|\d+)\s+will\s+allow\s+the\s+pair\s+to\s+continue\s+(rising|declining)\s+to\s+the\s+levels\s+of\s+(\d+\.\d+|\d+)\s*(&ndash;|–|-)\s*(\d+\.\d+|\d+))";

            Console.WriteLine($"Parsing text: {text}");
            Match match = Regex.Match(text, pattern);

            if (match.Success)
            {
                Console.WriteLine("Match found.");
                if (scenarioType == "Main scenario")
                {
                    scenario.Action = match.Groups[2].Value;
                    scenario.EntryPrice = double.Parse(match.Groups[4].Value, NumberStyles.Any, CultureInfo.InvariantCulture);
                    scenario.Takeprofit = double.Parse(match.Groups[5].Value, NumberStyles.Any, CultureInfo.InvariantCulture);
                    if (scenario.Takeprofit > scenario.EntryPrice)
                        scenario.Action = "long";
                    else scenario.Action = "short";


                }



                else
                {

                    scenario.EntryPrice = double.Parse(match.Groups[11].Value, NumberStyles.Any, CultureInfo.InvariantCulture);
                    scenario.Takeprofit = double.Parse(match.Groups[13].Value, NumberStyles.Any, CultureInfo.InvariantCulture);
                    if (scenario.Takeprofit > scenario.EntryPrice)
                        scenario.Action = "long";
                    else scenario.Action = "short";
                }
            }
            else
            {
                Console.WriteLine("No match found.");
            }

            return scenario;
        }
    }
}
