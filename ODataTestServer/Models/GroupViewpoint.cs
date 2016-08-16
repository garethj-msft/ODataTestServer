using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ODataTestServer.Models
{
    public abstract class GroupViewpointBase
    {
        public bool IsFavorite { get; set; }
        public int UnseenCount { get; set; } 
        public bool IsSubscribedByMail { get; set; }
    }

    /// <summary>
    /// Entity version of Group Viewpoint
    /// </summary>
    public class GroupViewpoint : GroupViewpointBase
    {
        [Key]
        public string Id { get; set; }

        public Group Group { get; set; }
        public User User { get; set; }

        public GroupViewpoint(Group group, User user)
        {
            Group = group;
            User = user;
            Id = user.Id;
        }

        internal GroupViewpointFacade AsFacade()
        {
            return new GroupViewpointFacade { IsFavorite = this.IsFavorite, UnseenCount = this.UnseenCount, IsSubscribedByMail = this.IsSubscribedByMail };
        }
    }

    /// <summary>
    /// Facade version of Group Viewpoint
    /// </summary>
    [ComplexType]
    public class GroupViewpointFacade : GroupViewpointBase
    {

    }
}