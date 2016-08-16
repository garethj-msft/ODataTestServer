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
        internal static GroupViewpoint viewpoint1_1, viewpoint1_3, viewpoint2_1, viewpoint2_2, viewpoint3_2, viewpoint3_3 ;

        internal static User CallerIdentity { get; private set; }

        private static List<Group> groups;
        private static List<User> users;
        private static List<GroupViewpoint> groupViewpoints;

        static Model()
        {
            Random random = new Random();
            user1 = new User { Id = "febfef90-9b5b-4aff-93f0-f8d476964d66", FirstName = "Bob", LastName = "Buzzard" };  // Easily referenceable fixed guid for one of them.
            user2 = new User { FirstName = "Stephen", LastName = "Daker" };
            user3 = new User { FirstName = "Rose", LastName = "Marie" };
            group1 = new Group { Id = "aebfef90-9b5b-4aff-93f0-f8d476964d66", DisplayName = "Medical Staff" }; // Easily referenceable fixed guid for one of them.
            group2 = new Group { DisplayName = "Faculty" };
            group3 = new Group { DisplayName = "Student Advisors" };
            viewpoint1_1 = new GroupViewpoint(group1, user1) { IsFavorite = true, UnseenCount = random.Next(11), IsSubscribedByMail = false };
            viewpoint1_3 = new GroupViewpoint(group1, user3) { IsFavorite = false, UnseenCount = random.Next(11), IsSubscribedByMail = true };
            viewpoint2_1 = new GroupViewpoint(group2, user1) { IsFavorite = false, UnseenCount = random.Next(11), IsSubscribedByMail = false };
            viewpoint2_2 = new GroupViewpoint(group2, user2) { IsFavorite = true, UnseenCount = random.Next(11), IsSubscribedByMail = true };
            viewpoint3_2 = new GroupViewpoint(group3, user2) { IsFavorite = true, UnseenCount = random.Next(11), IsSubscribedByMail = false };
            viewpoint3_3 = new GroupViewpoint(group3, user3) { IsFavorite = false, UnseenCount = random.Next(11), IsSubscribedByMail = true };

            user1.MemberOf.AddRange(new[] { group1, group2 });
            user2.MemberOf.AddRange(new[] { group2, group3 });
            user3.MemberOf.AddRange(new[] { group3, group1 });

            group1.Members.AddRange(new[] { user1, user3 });
            group2.Members.AddRange(new[] { user1, user2 });
            group3.Members.AddRange(new[] { user2, user3 });
            group1.Viewpoints.AddRange(new[] { viewpoint1_1, viewpoint1_3 });
            group2.Viewpoints.AddRange(new[] { viewpoint2_1, viewpoint2_2 });
            group3.Viewpoints.AddRange(new[] { viewpoint3_2, viewpoint3_3 });

            groups = new List<Group> { group1, group2, group3 };
            users = new List<User> { user1, user2, user3 };
            groupViewpoints = new List<GroupViewpoint> { viewpoint1_1, viewpoint1_3, viewpoint2_1, viewpoint2_2, viewpoint3_2, viewpoint3_3 };

            // Simulate Auth'd API call 
            CallerIdentity = user1;
        }

        internal static List<Group> Groups { get { return groups; } }
        internal static List<User> Users { get { return users; } }
        internal static List<GroupViewpoint> GroupViewpoints {  get { return groupViewpoints; } }
    }
}