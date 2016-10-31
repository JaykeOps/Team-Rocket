using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects
{
    public abstract class ValueObject
    {
        public abstract override bool Equals(object obj);
        public abstract override int GetHashCode();
        
    }
}
