using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTests.Value_Objects.Tests
{
    public partial class ValueObjectTests
    {
        public partial class TestObjectWithStringList : ValueObject<TestObjectWithStringList>
        {
            public string TestStr { get; set; } = "This is a test!";
            public int TestInt { get; set; } = 1337;
            public List<string> TestListOfStrings { get; set; } = new List<string> { "Test", "Test...", "Test......" };

            public override int GetHashCode()
            {
                return 0;
            }
        }

        public class TestObjectWithIntegerList : ValueObject<TestObjectWithIntegerList>
        {
            public List<int> TestListOfIntegers { get; set; } = new List<int> { 1, 2, 3, 4, 5 };

            public override int GetHashCode()
            {
                return 0;
            }
        }

        public class TestObjectWithDictionary : ValueObject<TestObjectWithDictionary>
        {
            public Dictionary<string, int> TestDictionary { get; set; } = new Dictionary<string, int>
            {
                {"Hello", 1 },
                {"World", 2 },
                {"!", 3 }
            };

            public override int GetHashCode()
            {
                return 0;
            }
        }
    }
}
