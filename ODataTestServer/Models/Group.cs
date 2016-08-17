using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ODataTestServer.Models
{
    public class Group
    {
        public List<User> Members { get; } = new List<User>();

        internal List<GroupViewpoint> viewpoints = new List<GroupViewpoint>(); // In reality would be stored with the User due to sharding.

        /// <summary>
        /// Collection of applicable viewpoints - contains one entry for delegate scenario and a collection for appOnly scenario.
        /// </summary>
        public IEnumerable<GroupViewpoint> Viewpoints
        {
            get
            {
                return GetViewpointsImpl();
            }
        }

        /// <summary>
        /// Internal version of Viewpoints to make debugging easier to separate external calls from internal ones.
        /// </summary>
        public IEnumerable<GroupViewpoint> InternalViewpoints
        {
            get
            {
                return GetViewpointsImpl();
            }
        }

        private IEnumerable<GroupViewpoint> GetViewpointsImpl()
        {
            // @@@ TODO: This and other code with similar CallerIdentity-centric tests is highly problematic, because we've got service code (auth) in our POCOs.
            // Inject the tests with some kind of hook, as it doesn't appear we can hook OData itself.
            return viewpoints.Where(vp => Model.CallerIdentity == null ? true : vp.User.Id == Model.CallerIdentity.Id);
        }

        [Key]
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public Group()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}