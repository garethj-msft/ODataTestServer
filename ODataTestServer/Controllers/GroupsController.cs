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
        public IHttpActionResult Get(string appOnly = null)
        {
            Model.SetupCallerIdentity(appOnly);
            return Ok(Model.Groups.AsQueryable());
        }

        // GET api/groups/<guid>
        [ODataRoute("groups({id})")]
        public IHttpActionResult GetGroup([FromODataUri]string id, string appOnly = null)
        {
            Model.SetupCallerIdentity(appOnly);
            return OkOrNotFound(Model.Groups.Where(g => g.Id == id).SingleOrDefault());
        }

        // GET api/groups/<guid>/viewpoints/$ref
        [ODataRoute("groups({id})/viewpoints/$ref")]
        public IHttpActionResult GetGroupViewpointsByRef([FromODataUri]string id, string appOnly = null)
        {
            Model.SetupCallerIdentity(appOnly);

            Group found = Model.Groups.Where(g => g.Id == id).SingleOrDefault();
            if (found != null)
            {
                var urlHelper = Request.GetUrlHelper() ?? new UrlHelper(Request);
                var segments = new List<ODataPathSegment>(Request.ODataProperties().Path.Segments);
                var uris = new List<Uri>();
                // Delete the $ref segment and replace it with each viewpoint's ID.
                foreach (GroupViewpoint viewpoint in found.Viewpoints)
                {
                    segments.RemoveAt(segments.Count - 1);
                    segments.Add(new KeyValuePathSegment($"'{viewpoint.Id}'"));
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
            Model.SetupCallerIdentity(appOnly);

            Group found = Model.Groups.Where(g => g.Id == id).SingleOrDefault();
            if (found != null)
            {
                return Ok(found.Viewpoints);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/groups/<guid>/viewpoint()
        [ODataRoute("groups({id})/viewpoint()")]
        [HttpGet]
        public IHttpActionResult Viewpoint([FromODataUri]string id)
        {
            Model.SetupCallerIdentity(null);  // Can't ask for this in appOnly

            Group found = Model.Groups.Where(g => g.Id == id).SingleOrDefault();
            if (found != null)
            {
                return Ok(found.Viewpoints);
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
            Model.SetupCallerIdentity(appOnly);

            Group found = Model.Groups.Where(g => g.Id == id).SingleOrDefault();
            if (found != null)
            {
                return OkOrNotFound(found.Viewpoints.Where(v => v.User.Id == vpid).SingleOrDefault());
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
