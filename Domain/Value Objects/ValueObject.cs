using System.Collections.Generic;
namespace Domain.Value_Objects
{
    public abstract class ValueObject<T>
        where T : ValueObject<T>
    {
        //public abstract override bool Equals(object obj);

        public abstract override int GetHashCode();

        public override bool Equals(object obj)
        {
            var bools = new List<bool>();
            //var valueObject = obj as T;
            if (ReferenceEquals(obj, null))
            {
                return false;
            }
            else
            {
                var props = obj.GetType().GetProperties();
                foreach (var prop in props)
                {
                    var value1 = prop.GetValue(obj,null);
                    var value2 = this.GetType().GetProperty(prop.Name).GetValue(this,null);
                    if (value1 == value2) // Here's the problem!!
                    {
                        bools.Add(true);
                    }
                    else
                    {
                            bools.Add(false);
                    }
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
        public static bool operator !=(ValueObject<T>objOne, ValueObject<T> objTwo)
        {
            return !(objOne == objTwo);
        }
    }
}