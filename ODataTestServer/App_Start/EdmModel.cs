using Microsoft.OData.Edm;
using ODataTestServer.Models;
using System.Web.OData.Builder;

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
