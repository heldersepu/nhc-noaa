using Microsoft.WindowsAzure.Storage.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace nhc_noaa.Controllers
{
    public class ImagesController : BaseController
    {
        [HttpGet]
        public IEnumerable<string> EastAtlantic(int count = 20, DateTime? min = null, DateTime? max = null)
        {
            return DirInfo.GetLatestFiles(count, min, max).OrderByDescending(x => x.Name).Select(x => x.Name);
        }

        [HttpPost]
        public IEnumerable<string> ImageData(int count = 20, DateTime? min = null, DateTime? max = null)
        {            
            var files = CloudDir.ListFilesAndDirectories().OrderByDescending(x => ((CloudFile)x).Name).Take(count);
            return files.Select(x => ((CloudFile)x).Name);
        }
    }
}
