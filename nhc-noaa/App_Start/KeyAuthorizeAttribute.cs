using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace nhc_noaa
{
    public class KeyAuthorizeAttribute : AuthorizeAttribute
    {
        static string _password = null;
        private string Password
        {
            get
            {
                if (string.IsNullOrEmpty(_password))
                    _password = ConfigurationManager.AppSettings["PASSWORD"];
                return _password;
            }
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            IEnumerable<string> apiKey;
            if (actionContext.Request.Headers.TryGetValues("apiKey", out apiKey))
            {
                return apiKey.First() == Password;
            }
            return false;
        }
    }
}