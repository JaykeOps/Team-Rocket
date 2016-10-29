using Domain.Value_Objects;
using System;

namespace football_series_manager.Domain.Entities
{
    public abstract class Person
    {
        public Person(Name name, DateOfBirth dateOfBirth, PersonContactInformation contactInformation)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        public Guid Id { get; }
        public Name Name { get; }
        public DateOfBirth DateOfBirth { get; }
        public PersonContactInformation PersonContactInformation { get; }

        public override string ToString()
        {
            return $"{this.Name.FirstName} {this.Name.LastName}";
        }
    }
}