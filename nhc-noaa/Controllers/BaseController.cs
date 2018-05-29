using System;
using System.IO;
using System.Web.Http;
using System.Collections.Generic;
using System.Configuration;

namespace nhc_noaa.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected string Domain { get { return ConfigurationManager.AppSettings["DOMAIN"]; } }
        protected string Year { get { return DateTime.Now.Year.ToString(); } }
        protected string EastAtlPath { get { return ConfigurationManager.AppSettings["EAST_ATL"]; } }
        protected string Images { get { return @">" + Year + ".*1800x1080.jpg"; } }

        static protected string BaseDir(string path)
        {
            string fld = AppDomain.CurrentDomain.BaseDirectory;
            fld += path.Trim('/').Replace("/", "_");
            if (!Directory.Exists(fld))
                Directory.CreateDirectory(fld);
            return fld;
        }

        protected DirectoryInfo DirInfo
        {
            get
            {
                return new DirectoryInfo(BaseDir(EastAtlPath));
            }
        }
    }

    public class Images : Dictionary<string, int> { }
}
