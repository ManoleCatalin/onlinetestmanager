using System.Collections.Generic;

namespace Constants
{
    public static class RoleConstants
    {
        private static readonly string[] RoleNames = { "Teacher", "Student" };

        public static readonly string TeacherRoleName = RoleNames[0];
        public static readonly string StudentRoleName = RoleNames[1];

        public static List<string> GetRoleNames()
        {
            return new List<string>(RoleNames);
        }
    }
}
