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
    public class UsersController : ODataController
    {
       

        // GET api/users
        public IHttpActionResult Get()
        {
            return Ok(Model.Users);
        }

        // GET api/users/<guid>
        [ODataRoute("users({id})")]
        public IHttpActionResult GetUser([FromODataUri]string id)
        {

            User found = Model.Users.Where(u => u.Id == id).SingleOrDefault();
            if (found != null)
            {
                return Ok(found);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/users/<guid>/memberOf
        [ODataRoute("users({id})/memberOf")]
        public IHttpActionResult GetUserMemberOf([FromODataUri]string id, string appOnly = null)
        {
            Model.SetupCallerIdentity(appOnly);

            User found = Model.Users.Where(u => u.Id == id).SingleOrDefault();
            if (found != null)
            {
                return Ok(found.MemberOf);
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
