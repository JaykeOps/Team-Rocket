using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    [Serializable]
    public abstract class ValueObject<T> : IEquatable<T>
        where T : ValueObject<T>
    {
        public override bool Equals(object obj)

        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as T;
            return this.Equals(other);
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
                        if (propertyValueOfInputObject is IDictionary)
                        {
                            booleans.Add(this.DictionariesValueAreEqual(propertyValueOfInputObject,
                                propertyValueOfThisObject));
                        }
                        else
                        {
                            booleans.Add(this.ListsValuesAreEqual(propertyValueOfInputObject,
                            propertyValueOfThisObject));
                        }
                    }
                    else
                    {
                        booleans.Add(propertyValueOfInputObject.Equals(propertyValueOfThisObject));
                    }
                }
                return !booleans.Contains(false);
            }
        }

        private bool DictionariesValueAreEqual(object propertyValueOfInputObject,
            object propertyValueOfThisObject)
        {
            var booleans = new List<bool?>();
            var inputDictionaryObject = (IDictionary)propertyValueOfInputObject;
            var thisDictionaryObject = (IDictionary)propertyValueOfThisObject;

            if (inputDictionaryObject.Count != thisDictionaryObject.Count)
            {
                return false;
            }
            else
            {
                foreach (var key in inputDictionaryObject.Keys)
                {
                    var inputValue = inputDictionaryObject?[key];
                    var thisValue = thisDictionaryObject?[key];
                    booleans.Add(inputValue.Equals(thisValue));
                }
                return !booleans.Contains(false) && !booleans.Contains(null);
            }
        }

        private bool ListsValuesAreEqual(object propertyValueOfInputObject,
            object propertyValueOfThisObject)
        {
            var booleans = new List<bool?>();
            var inputListObject = (IList)propertyValueOfInputObject;
            var thisListObject = (IList)propertyValueOfThisObject;
            if (inputListObject.Count != thisListObject.Count)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < inputListObject.Count; i++)
                {
                    booleans.Add(inputListObject?[i].Equals(thisListObject?[i]));
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

        public override int GetHashCode()
        {
            var properties = this.GetType().GetProperties();
            int hashCode = 0;
            foreach (var property in properties)
            {
                if (property.PropertyType.Namespace != "System.Collections.Generic" && property.GetValue(this, null) != null)
                {
                    hashCode += property.GetValue(this, null).GetHashCode();
                }
                else if (property.GetValue(this, null) != null)
                {
                    if (property.PropertyType.FullName.Contains("System.Collections.Generic.HashSet"))
                    {
                        hashCode += property.GetValue(this, null).GetHashCode();
                    }
                    else
                    {
                        hashCode += this.GetHashCodeFromAllListItems((ICollection)property.GetValue(this, null));
                    }
                }
            }
            return hashCode;
        }

        private int GetHashCodeFromAllListItems(ICollection collectionProperty)
        {
            int hashCode = 0;
            if (collectionProperty != null)
            {
                foreach (var item in collectionProperty)
                {
                    hashCode += item.GetHashCode();
                }
            }

            return hashCode;
        }
    }
}