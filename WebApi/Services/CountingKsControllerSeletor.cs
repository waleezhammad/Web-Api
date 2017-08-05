using System;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace WebApi.Services
{
    public class CountingKsControllerSeletor : DefaultHttpControllerSelector
    {
        private HttpConfiguration _config;

        public CountingKsControllerSeletor(HttpConfiguration config):base(config)
        {
            _config = config;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var controllers = GetControllerMapping();

            var routData = request.GetRouteData();

            var controllerName = routData.Values["controller"].ToString();

            HttpControllerDescriptor controllerDescriptor;
            if(controllers.TryGetValue(controllerName,out controllerDescriptor))
            {
                var versionName = GetFromAccept(request);
                    //GetVersionFromHeader(request);
                    //GetVersionFromQueryString(request);

                string newControllerName = controllerName + "v" + versionName;

                HttpControllerDescriptor versionDescripto;
                if (controllers.TryGetValue(newControllerName, out versionDescripto))
                    return versionDescripto;

                return controllerDescriptor;
            }

            return null;
        }

        private string GetFromAccept(HttpRequestMessage request)
        {
            var accept = request.Headers.Accept;
            foreach (var mime in accept)
            {
                if (mime.MediaType == "application/json")
                {
                    string value = mime.Parameters.Where(v => v.Name == "version").FirstOrDefault().Value;
                    return value;
                }
            }
            return "1";
        }

        private string GetVersionFromHeader(HttpRequestMessage request)
        {
            var headerName = "CountingKs-Version";
            if(request.Headers.Contains(headerName))
            {
                var hederValue = request.Headers.GetValues(headerName).First();
                if (hederValue != null)
                    return hederValue;
            }
            return "1";
        }

        private string GetVersionFromQueryString(HttpRequestMessage request)
        {
            var requestQueryString = HttpUtility.ParseQueryString(request.RequestUri.Query);

            var version = requestQueryString["v"];

            if (version != null)
                return version;

            return "1";

        }
    }
}
