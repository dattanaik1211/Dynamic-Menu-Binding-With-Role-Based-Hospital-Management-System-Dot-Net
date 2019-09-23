using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Framework
{
    public class NullableComparer<T> : IEqualityComparer<T?> where T : struct
    {
        public bool Equals(T? x, T? y)
        {
            if (!x.HasValue || !y.HasValue)
            {
                return false;
            }

            return x.Value.Equals(y.Value);
        }

        public int GetHashCode(T? obj)
        {
            return obj.GetHashCode();
        }
    }
}