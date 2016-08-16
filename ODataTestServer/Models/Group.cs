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
                // @@@ TODO: This is highly problematic, because we've got service code (auth) in our POCOs.
                return viewpoints.Where(vp => Model.CallerIdentity == null ? true : vp.User.Id == Model.CallerIdentity.Id);
            }
        }

        [Key]
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public Group()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Facade of the viewpoint for delegate scenarios - null in appOnly scenarios
        /// </summary>
        public GroupViewpointFacade Viewpoint
        {
            get
            {
                return Model.CallerIdentity == null ? null : Viewpoints.SingleOrDefault()?.AsFacade();
            }
            set
            {
            }
        }
    }
}