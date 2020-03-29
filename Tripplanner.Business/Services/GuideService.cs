using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private const string GuideFolder = "Guides";
        private HttpClient httpClient;
        private IStorageService storageService;

        public GuideService(IStorageService storageService)
        {
            this.storageService = storageService;
            httpClient = new HttpClient();
        }

        public async Task<IEnumerable<GuideSection>> GetAllSections(string location)
        {
            var sections = await GetSectionsFromCache(location);
            if(sections.Any())
            {
                return sections;
            }

            try
            {
                sections = await GetSectionsFromApi(location);
            }
            catch (Exception ex)
            {

                throw;
            }

            await SaveLocationToCache(location, sections);

            return sections;
        }

        public async Task<string> GetSectionByIndex(string location, int index)
        {
            var requestUrl = $"{BaseUrl}&page={location}&section={index}";
            var result = await httpClient.GetStringAsync(requestUrl);
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

        private async Task<IEnumerable<GuideSection>> GetSectionsFromCache(string location)
        {
            var path = Path.Combine(GuideFolder, $"{EscapeFileName(location)}.json");
            if(storageService.FileExists(path))
            {
                var rawContent = await Task.Run(() => storageService.LoadTextFile(path));
                var guide = JsonConvert.DeserializeObject<LocationGuide>(rawContent);
                return guide.Sections;
            }
            return Array.Empty<GuideSection>();
        }

        private async Task SaveLocationToCache(string location, IEnumerable<GuideSection> sections)
        {
            var guide = new LocationGuide
            {
                Location = location,
                Sections = sections
            };
            var guideContent = JsonConvert.SerializeObject(guide);
            await Task.Run(() => storageService.SaveTextFile(Path.Combine(GuideFolder, $"{EscapeFileName(location)}.json"), guideContent));
        }

        private string EscapeFileName(string fileName)
        {
            return string.Join("_", fileName.Split(Path.GetInvalidFileNameChars()));
        }

        private async Task<IEnumerable<GuideSection>> GetSectionsFromApi(string location)
        {
            var requestUrl = $"{BaseUrl}&page={location}&prop=sections";
            var result = await httpClient.GetStringAsync(requestUrl);
            var json = JsonConvert.DeserializeObject<JObject>(result);
            var rawSections = json["parse"]["sections"] as JArray;
            var sections = new List<GuideSection>();
            foreach (var item in rawSections)
            {
                var index = item.Value<int>("index");
                var content = await GetSectionByIndex(location, index);
                var section = new GuideSection
                {
                    Id = item.Value<string>("anchor"),
                    Title = item.Value<string>("line"),
                    Index = index,
                    Level = item.Value<int>("level"),
                    PageId = item.Value<string>("fromtitle"),
                    HtmlContent = content
                };
                sections.Add(section);
            }
            return sections;
        }
    }
}
