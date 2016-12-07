using Domain.Value_Objects;
using System;

namespace Domain.Entities
{
    [Serializable]
    public abstract class Person
    {
        public Guid Id { get; }
        public Name Name { get; set; }
        public DateOfBirth DateOfBirth { get; set; }
        public ContactInformation ContactInformation { get; }

        public Person(Name name, DateOfBirth dateOfBirth)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this.ContactInformation = new ContactInformation();
        }

        public Person(Name name, DateOfBirth dateOfBirth, Guid id) // For tests!
        {
            this.Id = id; // for tests!
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
        }

        public override string ToString()
        {
            return $"{this.Name.FirstName} {this.Name.LastName}";
        }
    }
}