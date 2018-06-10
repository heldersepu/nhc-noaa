using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;

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
            var storageAccount = CloudStorageAccount.Parse(ConnectionString);
            var fileClient = storageAccount.CreateCloudFileClient();
            var share = fileClient.GetShareReference("images");
            var dir = share.GetRootDirectoryReference();
            var files = dir.ListFilesAndDirectories().OrderByDescending(x => ((CloudFile)x).Name).Take(count);
            return files.Select(x => ((CloudFile)x).Name);
        }
    }
}
