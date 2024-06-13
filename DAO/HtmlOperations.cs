using HtmlAgilityPack;
using Azurtesting.helpers;
using Azurtesting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Azurtesting.DAO
{
    public class HtmlOperations
    {

        public HtmlOperations() { }
        public HtmlDocument Load(string url)
        {
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            return web.Load(url);
        }
        public List<Azurtesting.Models.Entry> GetEntries(HtmlDocument doc, string query)
        {
            List<Azurtesting.Models.Entry> io = new List<Azurtesting.Models.Entry>();
            // Use XPath to find the desired div
            var divNodes = doc.DocumentNode.SelectNodes("//div[@class='columns__item-content']");

            if (divNodes != null)
            {
                foreach (var div in divNodes)
                {
                    // Extract the title from the "title" attribute in the "div" with class "title"
                    var titleNode = div.SelectSingleNode(".//div[contains(@class, 'title')]");
                    string title = titleNode?.GetAttributeValue("title", "No title attribute found");

                    if ((title ?? "").Trim().ToUpper().StartsWith(query.ToUpper()))
                    {
                        // Extract the URL from the "a" tag within the "div" with class "title"
                        string link = titleNode?.SelectSingleNode(".//a")?.GetAttributeValue("href", "No link found");
                        Models.Entry selectedentry = new Models.Entry()
                        {
                            Titel = title,
                            Url = link

                        };

                        SetSCenario(selectedentry);

                        io.Add(selectedentry);

                    }
                }

            }
            return io;
        }

        public void SetSCenario(Entry a)
        {
            HtmlAgilityPack.HtmlWeb web2 = new HtmlAgilityPack.HtmlWeb();
            HtmlDocument doc = web2.Load(a.Url);

            var contentBoxNode = doc.DocumentNode.SelectSingleNode("//div[@class='content-box']");
            // XPath pour trouver les paragraphes contenant les scénarios
            var mainScenarioNode = doc.DocumentNode.SelectSingleNode("//p[strong[contains(text(), 'Main scenario:')]]");
            var alternativeScenarioNode = doc.DocumentNode.SelectSingleNode("//p[strong[contains(text(), 'Alternative scenario:')]]");

            // Extraction du texte des scénarios
            string mainScenario = WebUtility.HtmlDecode(mainScenarioNode?.InnerText.Trim());
            string alternativeScenario = WebUtility.HtmlDecode(alternativeScenarioNode?.InnerText.Trim());

            a.MainScenario = new Tools().ParseScenario(mainScenario, "Main scenario");
            a.AltrentiveScenario = new Tools().ParseScenario(alternativeScenario, "Alternative scenario");
        }



    }
}