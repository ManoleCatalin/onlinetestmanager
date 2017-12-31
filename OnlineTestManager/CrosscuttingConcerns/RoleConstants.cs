using System.Collections.Generic;

namespace Constants
{
    public static class RoleConstants
    {
        public const string TeacherRoleName = "Teacher";
        public const string StudentRoleName = "Student";

        private static readonly string[] RoleNames = { TeacherRoleName, StudentRoleName };

        public static List<string> GetRoleNames()
        {
            return new List<string>(RoleNames);
        }
    }
}
