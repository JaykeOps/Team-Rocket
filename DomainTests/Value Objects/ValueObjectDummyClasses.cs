using Domain.Value_Objects;
using System.Collections.Generic;

namespace DomainTests.Value_Objects.Tests
{
    public class TestObject : ValueObject<TestObject>
    {
        public string TestStr { get; set; } = "This is a test!";
        public int TestInt { get; set; } = 1337;
    }

    public class TestObjectWithStringList : ValueObject<TestObjectWithStringList>
    {
        public List<string> TestListOfStrings { get; set; } = new List<string> { "Test", "Test...", "Test......" };
    }

    public class TestObjectWithIntegerList : ValueObject<TestObjectWithIntegerList>
    {
        public List<int> TestListOfIntegers { get; set; } = new List<int> { 1, 2, 3, 4, 5 };
    }

    public class TestObjectWithDictionary : ValueObject<TestObjectWithDictionary>
    {
        public Dictionary<string, int> TestDictionary { get; set; } = new Dictionary<string, int>
            {
                {"Hello", 1 },
                {"World", 2 },
                {"!", 3 }
            };
    }
}