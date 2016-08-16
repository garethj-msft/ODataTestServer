using ODataTestServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;

namespace ODataTestServer.Controllers
{
    [EnableQuery]
    public class GroupsController : ODataController
    {
       

        // GET api/groups
        public IHttpActionResult Get()
        {
            return Ok(Model.Groups);
        }

        // GET api/groups/<guid>
        [ODataRoute("groups({id})")]
        public IHttpActionResult GetGroup([FromODataUri]string id)
        {

            Group found = Model.Groups.Where(g => g.Id == id).SingleOrDefault();
            if (found != null)
            {
                return Ok(found);
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
                    return Ok(found.Viewpoints.Where(v => v.User == Model.CallerIdentity));
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
    }
}
