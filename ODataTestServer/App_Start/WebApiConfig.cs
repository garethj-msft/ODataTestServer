using System.Web.Http;
using System.Web.OData.Extensions;
using Microsoft.OData.Core.UriParser;

namespace ODataTestServer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // OData routes
            config.SetUrlConventions(ODataUrlConventions.ODataSimplified);
            config.EnableEnumPrefixFree(true);
            config.EnableUnqualifiedNameCall(true);
            config.EnableCaseInsensitive(true);
            config.MapODataServiceRoute("api", "api", EdmModel.GetModel());
        }
    }
}
