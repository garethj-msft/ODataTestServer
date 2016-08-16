using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ODataTestServer.Models
{
    public class Group
    {
        public List<User> Members { get; } = new List<User>();

        public List<GroupViewpoint> Viewpoints { get; } = new List<GroupViewpoint>();  // In reality would be stored with the User due to sharding.

        [Key]
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public Group()
        {
            this.Id = Guid.NewGuid().ToString();
        }

    }
}