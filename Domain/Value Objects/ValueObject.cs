using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    public abstract class ValueObject<T> : IEquatable<T>
        where T : ValueObject<T>
    {
        public abstract override int GetHashCode();

        public override bool Equals(object obj)

        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as T;
            return Equals(other);
        }

        public virtual bool Equals(T obj)
        {
            var bools = new List<bool>();

            if (ReferenceEquals(obj, null) || obj.GetType() != typeof(T))
            {
                return false;
            }
            else
            {
                var props = obj.GetType().GetProperties();
                foreach (var prop in props)
                {
                    var value1 = prop.GetValue(obj, null);
                    var value2 = this.GetType().GetProperty(prop.Name).GetValue(this, null);

                    bools.Add(value1.Equals(value2));
                }
            }
            return !bools.Contains(false);
        }

        public static bool operator ==(ValueObject<T> objOne, ValueObject<T> objTwo)
        {
            if (ReferenceEquals(objOne, null) && ReferenceEquals(objTwo, null))
            {
                return true;
            }
            else if (ReferenceEquals(objOne, null) || ReferenceEquals(objTwo, null))
            {
                return false;
            }
            else
            {
                return objOne.Equals(objTwo);
            }
        }

        public static bool operator !=(ValueObject<T> objOne, ValueObject<T> objTwo)
        {
            return !(objOne == objTwo);
        }
    }
}