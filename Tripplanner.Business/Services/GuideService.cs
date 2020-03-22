using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tripplanner.Business.Models;

namespace Tripplanner.Business.Services
{
    public class GuideService : IGuideService
    {
        private const string BaseUrl = "https://wikitravel.org/wiki/en/api.php?action=parse&mobileformat=true&format=json";
        private HttpClient httpClient = new HttpClient();

        public async Task<IEnumerable<GuideSection>> GetAllSections(string location)
        {
            var requestUrl = $"{BaseUrl}&page={location}&prop=sections";
            var result = httpClient.GetStringAsync(requestUrl).Result;
            var json = JsonConvert.DeserializeObject<JObject>(result);
            var rawSections = json["parse"]["sections"] as JArray;
            var sections = new List<GuideSection>();
            foreach (var item in rawSections)
            {
                var section = new GuideSection
                {
                    Id = item.Value<string>("anchor"),
                    Title = item.Value<string>("line"),
                    Index = item.Value<int>("index"),
                    Level = item.Value<int>("level"),
                    PageId = item.Value<string>("fromtitle")
                };
                sections.Add(section);
            }
            return sections;
        }

        public async Task<string> GetSectionByIndex(string location, int index)
        {
            var requestUrl = $"{BaseUrl}&page={location}&section={index}";
            var result = httpClient.GetStringAsync(requestUrl).Result;
            var json = JsonConvert.DeserializeObject<JObject>(result);
            var textObj = json["parse"]["text"];
            var html = textObj.Value<string>("*");
            var sectionStart = html.IndexOf("<div class=\"mf-section-1\"");
            if(sectionStart >= 0)
            {
                html = html.Substring(sectionStart);
            }
            return html;
        }
    }
}
