using System;
using System.Collections;
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
            var booleans = new List<bool>();
            if (ReferenceEquals(obj, null) || obj.GetType() != typeof(T))
            {
                return false;
            }
            else
            {
                var properties = obj.GetType().GetProperties();
                foreach (var property in properties)
                {
                    var propertyValueOfInputObject = property.GetValue(obj, null);
                    var propertyValueOfThisObject = this.GetType().GetProperty(property.Name)
                        .GetValue(this, null);
                    if (property.PropertyType.Namespace == "System.Collections.Generic")
                    {
                        booleans.Add(this.CollectionsAreEqual(propertyValueOfInputObject,
                            propertyValueOfThisObject));
                    }
                    else
                    {
                        booleans.Add(propertyValueOfInputObject.Equals(propertyValueOfThisObject));
                    }
                }
            }
            return !booleans.Contains(false);
        }

        private bool CollectionsAreEqual(object propertyValueOfInputObject,
            object propertyValueOfThisObject)
        {
            var booleans = new List<bool?>();
            var inputCollectionObject = (IList)propertyValueOfInputObject;
            var thisCollectionObject = (IList)propertyValueOfThisObject;
            if (inputCollectionObject.Count != thisCollectionObject.Count)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < inputCollectionObject.Count; i++)
                {
                    booleans.Add(inputCollectionObject?[i].Equals(thisCollectionObject?[i]));
                }
                return !booleans.Contains(false) && !booleans.Contains(null);
            }
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