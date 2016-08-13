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
        internal static List<Group> groups = new List<Group> { new Group { Id = "aebfef90-9b5b-4aff-93f0-f8d476964d66" }, new Group() };  

        // GET api/groups
        public IHttpActionResult Get()
        {
            return Ok(groups);
        }

        // GET api/groups/<guid>
        [ODataRoute("groups({id})")]
        public IHttpActionResult GetGroup([FromODataUri]string id)
        {

            Group found = groups.Where(g => g.Id == id).SingleOrDefault();
            if (found != null)
            {
                return Ok(found);
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
