using Microsoft.OData.Core;
using Microsoft.OData.Core.UriParser;
using ODataTestServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.OData;
using System.Web.OData.Extensions;
using System.Web.OData.Routing;

namespace ODataTestServer.Controllers
{
    [EnableQuery]
    public class GroupsController : ODataController
    {
        // GET api/groups
        public IHttpActionResult Get()
        {
            return Ok(Model.Groups.AsQueryable());
        }

        // GET api/groups/<guid>
        [ODataRoute("groups({id})")]
        public IHttpActionResult GetGroup([FromODataUri]string id)
        {
            return OkOrNotFound(Model.Groups.Where(g => g.Id == id).SingleOrDefault());
        }

        // GET api/groups/<guid>/viewpoints/$ref
        [ODataRoute("groups({id})/viewpoints/$ref")]
        public IHttpActionResult GetGroupViewpointsByRef([FromODataUri]string id, string appOnly = null)
        {
            User callerIdentity = appOnly == null ? Model.CallerIdentity : null;

            Group found = Model.Groups.Where(g => g.Id == id).SingleOrDefault();
            if (found != null)
            {
                IEnumerable<GroupViewpoint> viewpoints = found.Viewpoints;
                if (callerIdentity != null)
                {
                    viewpoints = found.Viewpoints.Where(v => v.User == Model.CallerIdentity);
                }
                var urlHelper = Request.GetUrlHelper() ?? new UrlHelper(Request);
                var segments = new List<ODataPathSegment>(Request.ODataProperties().Path.Segments);
                var uris = new List<Uri>();
                // Delete the $ref segment and replace it with each viewpoint's ID.
                foreach (GroupViewpoint viewpoint in viewpoints)
                {
                    segments.RemoveAt(segments.Count - 1);
                    segments.Add(new KeyValuePathSegment($"'{viewpoint.UserId}'"));
                    string uriString = urlHelper.CreateODataLink(Request.ODataProperties().RouteName, Request.ODataProperties().PathHandler, segments);
                    // Passthru the appOnly flag
                    if (appOnly != null)
                    {
                        uriString += $"?appOnly={appOnly}";
                    }
                    uris.Add(new Uri(uriString));
                }
                return Ok(uris);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/groups/<guid>/viewpoints
        [ODataRoute("groups({id})/viewpoints")]
        public IHttpActionResult GetGroupViewpoints([FromODataUri]string id, string appOnly=null)
        {
            User callerIdentity = appOnly == null ? Model.CallerIdentity : null;

            Group found = Model.Groups.Where(g => g.Id == id).SingleOrDefault();
            if (found != null)
            {
                if (callerIdentity != null)
                {
                    return Ok(found.Viewpoints.Where(v => v.User == callerIdentity));
                }
                else
                {
                    return Ok(found.Viewpoints);
                }
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/groups/<guid>/viewpoints
        [ODataRoute("groups({id})/viewpoints({vpid})")]
        public IHttpActionResult GetGroupViewpoint([FromODataUri]string id, [FromODataUri]string vpid, string appOnly = null)
        {
            User callerIdentity = appOnly == null ? Model.CallerIdentity : null;

            Group found = Model.Groups.Where(g => g.Id == id).SingleOrDefault();
            if (found != null)
            {
                if (callerIdentity != null)
                {
                    return OkOrNotFound(found.Viewpoints.Where(v => v.User == callerIdentity && v.User.Id == vpid).SingleOrDefault());
                }
                else
                {
                    return OkOrNotFound(found.Viewpoints.Where(v => v.User.Id == vpid).SingleOrDefault());
                }
            }
            else
            {
                return NotFound();
            }
        }


        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        /// <summary>
        /// Helper method to get the key value from a uri.
        /// Usually used by $ref action to extract the key value from the url in body.
        /// </summary>
        /// <typeparam name="TKey">The type of the key</typeparam>
        /// <param name="request">The request instance in current context</param>
        /// <param name="uri">OData uri that contains the key value</param>
        /// <returns>The key value</returns>
        public static TKey GetKeyValue<TKey>(HttpRequestMessage request, Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            //get the odata path Ex: ~/prefix/entityset/key/navigation/$ref
            var odataRoute = request.GetRouteData().Route as ODataRoute;

            var odataLocalPath = string.IsNullOrEmpty(odataRoute.RoutePrefix) ? uri.LocalPath
                : uri.LocalPath.Substring(uri.LocalPath.IndexOf(odataRoute.RoutePrefix, StringComparison.InvariantCulture) + odataRoute.RoutePrefix.Length);

            var odataPath = request.ODataProperties().PathHandler.Parse(request.ODataProperties().Model, "root", odataLocalPath);
            var keySegment = odataPath.Segments.OfType<KeyValuePathSegment>().FirstOrDefault();
            if (keySegment == null)
            {
                throw new InvalidOperationException("The link does not contain a key.");
            }

            var value = ODataUriUtils.ConvertFromUriLiteral(keySegment.Value, ODataVersion.V4);
            return (TKey)value;
        }

        public IHttpActionResult OkOrNotFound<T>(T theObject)
        {
            if (theObject == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(theObject);
            }
        }
    }
}
