using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ODataTestServer.Models
{
    public class Group
    {

        public IEnumerable<User> Members { get; }

        [Key]
        public string Id { get; set; }

        public Group()
        {
            this.Id = Guid.NewGuid().ToString();
        }

    }
}