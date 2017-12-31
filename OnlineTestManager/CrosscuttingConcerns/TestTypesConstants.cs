using System.Collections.Generic;

namespace Constants
{
    public static class TestTypesConstants
    {
        public const string MultipleChoiceTestType = "MultipleChoice";

        private static readonly string[] s = { MultipleChoiceTestType };

        public static List<string> GetTestTypeNames()
        {
            return new List<string>(TestTypeNames);
        }
    }
}
