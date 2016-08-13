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

        [Key]
        public string Id { get; set; }

        public Group()
        {
            this.Id = Guid.NewGuid().ToString();
        }

    }
}