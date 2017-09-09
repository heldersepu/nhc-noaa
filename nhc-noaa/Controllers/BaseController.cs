using System;
using System.IO;
using System.Web.Http;
using System.Collections.Generic;
using System.Configuration;

namespace nhc_noaa.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected string domain { get { return ConfigurationManager.AppSettings["DOMAIN"]; } }
        protected string year { get { return DateTime.Now.Year.ToString(); } }
        protected string east_atl_path { get { return ConfigurationManager.AppSettings["EAST_ATL"]; } }
        protected string images { get { return @">" + year + ".*rb.jpg"; } }

        static protected string baseDir(string path)
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
                return new DirectoryInfo(baseDir(east_atl_path));
            }
        }
    }

    public class Images : Dictionary<string, int> { }
}
