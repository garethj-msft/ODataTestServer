using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ODataTestServer.Models
{
    public class User
    {
        public IEnumerable<Group> MemberOf { get; }

        [Key]
        public string Id { get; set;  }

        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}