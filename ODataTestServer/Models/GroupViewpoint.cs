using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ODataTestServer.Models
{
    public class GroupViewpoint
    {
        [Key]
        public string Id { get; set; }

        public Group Group { get; set; }
        public User User { get; set; }
        public bool IsFavorite { get; set; }
        public int UnseenCount { get; set; }
        public bool IsSubscribedByMail { get; set; }

        public GroupViewpoint(Group group, User user)
        {
            Group = group;
            User = user;
            Id = user.Id;
        }
    }
}