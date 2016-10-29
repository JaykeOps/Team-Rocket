using System;

namespace football_series_manager.Domain.Entities
{
    public abstract class Person
    {
        public Person(Name name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        protected Guid Id { get; }
        protected Name Name { get; }

        public override string ToString()
        {
            return $"{this.Name.FirstName} {this.Name.LastName}";
        }
    }
}