using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace football_series_manager.Domain.Entities
{
    public abstract class Person
    {
        public Person()
        {

        }

        public Guid Id { get; set; }
        public Name Name { get; set; }
    }
}
