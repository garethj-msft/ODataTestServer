using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.OData.Extensions;
using Microsoft.OData.Edm;
using ODataTestServer.Models;
using System.Web.OData.Builder;
using Microsoft.OData.Core.UriParser;

namespace ODataTestServer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.SetUrlConventions(ODataUrlConventions.ODataSimplified);
            config.EnableEnumPrefixFree(true);
            config.EnableUnqualifiedNameCall(true);
            config.EnableCaseInsensitive(true);
            config.MapODataServiceRoute("api", "api", EdmModel.GetModel());

        
        }
    }
}
