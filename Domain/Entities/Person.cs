using Domain.Value_Objects;
using System;

namespace football_series_manager.Domain.Entities
{
    public abstract class Person
    {
        public Person(Name name, DateOfBirth dateOfBirth, ContactInformation contactInformation)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this.ContactInformation = contactInformation;
        }

        public Guid Id { get; }
        public Name Name { get; }
        public DateOfBirth DateOfBirth { get; }
        public ContactInformation ContactInformation { get; }

        public override string ToString()
        {
            return $"{this.Name.FirstName} {this.Name.LastName}";
        }
    }
}