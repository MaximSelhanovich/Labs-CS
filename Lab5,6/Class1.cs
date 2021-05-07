using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    public interface IMyComparable<in T>
    {
       int MyCompareTo(T other);
    }

    static void Sort<T>(T[]array) where T : IComparable<T>
    {
        int i = 0;
        int j = 0;
        for (i = 0; i < array.Length; ++i)
        {
            for (i = 0; i < array.Length; ++i)
            { 
                if (array[i] > array[j])
                {
                    T temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
        }
    }

    class Class1 : IMyComparable<T>

    {

        public int CompareTo(T toCompare)
        {
            if (toCompare == null) return 1;

            if (toCompare < this) return -1;
            if (toCompare == this) return 0;
            return 1;
        }
    }
}
