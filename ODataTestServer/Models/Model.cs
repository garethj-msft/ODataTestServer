using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODataTestServer.Models
{
    public static class Model
    {
        internal static User user1, user2, user3;
        internal static Group group1, group2, group3;

        private static List<Group> groups;
        private static List<User> users;

        static Model()
        {
            user1 = new User { Id = "febfef90-9b5b-4aff-93f0-f8d476964d66" };  // Easily referenceable fixed guid for one of them.
            user2 = new User();
            user3 = new User();
            group1 = new Group { Id = "aebfef90-9b5b-4aff-93f0-f8d476964d66" }; // Easily referenceable fixed guid for one of them.
            group2 = new Group();
            group3 = new Group();

            user1.MemberOf.AddRange(new[] { group1, group2 });
            user2.MemberOf.AddRange(new[] { group2, group3 });
            user3.MemberOf.AddRange(new[] { group3, group1 });

            group1.Members.AddRange(new[] { user1, user3 });
            group2.Members.AddRange(new[] { user1, user2 });
            group3.Members.AddRange(new[] { user2, user3 });

            groups = new List<Group> { group1, group2, group3 };
            users = new List<User> { user1, user2, user3 };
        }

        internal static List<Group> Groups { get { return groups; } }
        internal static List<User> Users { get { return users; } }
    }
}