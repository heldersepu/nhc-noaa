using AviFile;
using NLog;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Http;

namespace nhc_noaa.Controllers
{
    public class VideosController : BaseController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [HttpGet]
        [KeyAuthorize]
        public dynamic EastAtlantic(int count = 20, DateTime? min = null, DateTime? max = null, double frameRate = 25, bool isCompressed = true)
        {
            DateTime sTime = DateTime.Now;
            string fileName = BaseDir("videos\\");
            var files = DirInfo.GetLatestFiles(count, min, max);
            if (files.Length > 1)
            {
                fileName += files[0].Name.Replace(".jpg", "") + "_" + files[files.Length - 1].Name.Replace(".jpg", "") + ".avi";
                if (!File.Exists(fileName))
                {
                    CreateVideo(fileName, files, frameRate, isCompressed);
                }
            }
            return Json(new
            {
                Time = sTime.Diff(),
                File = FileDetails(fileName)
            });
        }

        private dynamic FileDetails(string fileName)
        {
            var f = new FileInfo(fileName);
            return new {
                Name = f.Name,
                FullName = f.FullName,
                Size = f.Length,
                CreationTime = f.CreationTime
            };
        }

        private void CreateVideo(string fileName, FileInfo[] files, double frameRate, bool isCompressed)
        {
            try
            {
                var bmp = new Bitmap(1120, 480, PixelFormat.Format24bppRgb);
                var aviManager = new AviManager(fileName, false);
                var aviStream = aviManager.AddVideoStream(isCompressed, frameRate,  bmp);
                foreach (var file in files)
                {
                    bmp = (Bitmap)Bitmap.FromFile(file.FullName);
                    aviStream.AddFrame(bmp);
                    bmp.Dispose();
                }
                aviManager.Close();
            }
            catch (Exception e)
            {
                logger.Error(e);
                throw e;
            }
        }
    }
}
