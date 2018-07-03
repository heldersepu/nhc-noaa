using RestSharp;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;

namespace nhc_noaa.Controllers
{
    public class DownloadController : BaseController
    {
        [HttpGet]
        public async Task<dynamic> EastAtlantic()
        {
            DateTime sTime = DateTime.Now;
            dynamic obj = new ExpandoObject();
            obj.Images = await Download(Domain, EastAtlPath, Images);
            obj.Time = sTime.Diff();
            return Json(obj);
        }

        private async Task<Images> Download(string domain, string path, string pattern)
        {
            var result = new Images();
            var client = new RestClient(domain);
            var restTasks = new List<Task<IRestResponse>>();
            var response = client.Get(new RestRequest(path, Method.GET));

            foreach (Match match in Regex.Matches(response.Content, pattern))
            {
                string fileName = match.Captures[0].Value.Replace(">", "");
                result.Add(fileName, 0);
                if (!CloudDir.GetFileReference(fileName).Exists())
                {
                    var img = new RestRequest(path + fileName, Method.GET);
                    img.AddParameter("fileName", fileName);
                    restTasks.Add(client.ExecuteTaskAsync(img));
                }
            }

            foreach (var restTask in restTasks)
            {
                response = await restTask;
                string fileName = response.Request.Parameters[0].Value.ToString();
                result[fileName] = (int)response.StatusCode;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var filePath = BaseDir(path) + "\\" + fileName;
                    using (var fs = File.Create(filePath))
                    {
                        await fs.WriteAsync(response.RawBytes, 0, response.RawBytes.Length);
                    }
                    await (new FileInfo(filePath)).Upload(ConnectionString);
                }
            }
            return result;
        }
    }
}
