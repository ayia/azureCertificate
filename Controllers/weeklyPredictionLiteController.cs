using Azurtesting.DAO;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Azurtesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class weeklyPredictionLiteController : ControllerBase
    {
        

       

        [HttpGet()]
        public List<Azurtesting.Models.Entry> Get(string currency, Models.Entry div)
        {
            List<Azurtesting.Models.Entry> thelist = new List<Azurtesting.Models.Entry>();
            // The URL of the web page you want to scrape
            string url = "https://www.litefinance.org/blog/authors/alex-geuta/";
            // Regular expression pattern to match dates in the format dd.mm.yy
            string pattern = @"(\d{2}\.\d{2}\.\d{2})";
            /*   for (int i = 1; i < 2; i++)
               {*/
            // Create a new HtmlWeb instance
            HtmlOperations web = new HtmlOperations();
         
            string url2 = url;//+ i.ToString();
            // Load the web page
            HtmlDocument doc = web.Load(url2);
            List<Azurtesting.Models.Entry> a = web.GetEntries(doc, currency);


            foreach (Azurtesting.Models.Entry div55 in a)
            {

                // Extract the URL from the "a" tag within the "div" with class "title"
                string link = div55.Url;
                // Find matches
                MatchCollection matches = Regex.Matches(div55.Titel, pattern);
                if (matches.Count >= 2) // Check if at least two dates were found
                {
                    // Extract the start and end dates
                    div55.StartDate = matches[0].Value;
                    div55.EndDate = matches[1].Value;
                }




                //   }
                thelist.AddRange(a);
            }
            return thelist;
        }

    }
}
