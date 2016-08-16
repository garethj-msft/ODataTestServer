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
    public static class EdmModel
    {
        public static IEdmModel GetModel()
        {

            var builder = new ODataConventionModelBuilder();
            builder.EnableLowerCamelCase(); // into camel case json format.
            builder.EntityType<GroupViewpoint>();
            var groups = builder.EntitySet<Group>("groups");
            builder.EntitySet<User>("users");
            groups.EntityType.ContainsMany<GroupViewpoint>(g => g.Viewpoints);
            return builder.GetEdmModel();
        }
    }
}
